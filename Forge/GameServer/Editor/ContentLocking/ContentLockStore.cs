using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace GameServer.Editor.ContentLocking
{
    /// <summary>
    /// A collection of keys internally of content that has been locked
    /// </summary>
    public class ContentLockStore
    {
        // Current locks on a particular piece of content
        private Dictionary<int, NetConnection> _contentLocks = new Dictionary<int, NetConnection>(); 

        public bool TryAcquireLock(NetConnection connection, int ID)
        {
            // Trying to aqquire a lock for something you already have or you're not authorized to obtain
            if (_contentLocks.ContainsKey(ID))
                return false;

            // Noone has taken it - go ahead and grab this
            _contentLocks.Add(ID, connection);

            return true;

        }

        /// <summary>
        /// Release sall locks with an associated contention
        /// </summary>
        /// <param name="connection"></param>
        public void ReleaseLocks(NetConnection connection)
        {
            var keys = _contentLocks.Where(cLock => cLock.Value == connection).ToList();
            keys.ForEach(x => ReleaseLock(x.Value, x.Key));

        }

        public bool ReleaseLock(NetConnection connection, int ID)
        {
            // If they don't have a lock, they can't release it
            if (!HasLock(connection, ID))
                return false;

            // Release the lock
            _contentLocks.Remove(ID);

            return true;

        }

        /// <summary>
        /// Returns a list of all the locked content IDs for a particular store
        /// </summary>
        /// <returns></returns>
        public List<int> GetLockedContentIDs()
        {
            return _contentLocks.Keys.ToList();
        }

        /// <summary>
        /// Determines whether a particular connection has a lock on a piece of content
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool HasLock(NetConnection connection, int ID)
        {
            // If the key dosen't even exist, don't bother
            if (!_contentLocks.ContainsKey(ID))
                return false;

            return _contentLocks[ID] == connection;

        }

        public bool AnyoneHasLock(NetConnection connection, int ID)
        {
            return _contentLocks.ContainsKey(ID);
        }
    }
}
