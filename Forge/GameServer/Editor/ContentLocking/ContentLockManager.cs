using System;
using System.Collections.Generic;
using System.Linq;
using Inspire.Shared.Models.Enums;
using Lidgren.Network;

namespace GameServer.Editor.ContentLocking
{
    /// <summary>
    /// The content lock manager helps provide delegation
    /// </summary>
    public class ContentLockManager
    {

        // This is a mapping of all the different content lock stores available
        Dictionary<ContentType, ContentLockStore> _contentLockStores = new Dictionary<ContentType, ContentLockStore>();

        public ContentLockManager()
        {
            // Generate the ContentMap dynamically, assinging everyone a backing
            foreach (var contentType in GetValues<ContentType>())
                _contentLockStores.Add(contentType, new ContentLockStore());
        }

        /// <summary>
        /// Invalidates and purges all locks for a given connection.
        /// This is typically called when a connection has disconnected from the node.
        /// </summary>
        /// <param name="connection"></param>
        public void PurgeLocks(NetConnection connection)
        {
            foreach (var contentLockStore in _contentLockStores.Values)
            {
                contentLockStore.ReleaseLocks(connection);
            }
        }

        public List<int> GetLockedContent(ContentType contentType)
        {
            var contentStore = _contentLockStores[contentType];
            return contentStore.GetLockedContentIDs();
        }

        public bool HasLock(NetConnection connection, int ID, ContentType contentType)
        {
            var contentStore = _contentLockStores[contentType];
            return contentStore.HasLock(connection, ID);
        }

        public bool TryAcquireLock(NetConnection connection, int ID, ContentType contentType)
        {
            var contentStore = _contentLockStores[contentType];
            return contentStore.TryAcquireLock(connection, ID);
        }

        public bool TryReleaseLock(NetConnection connection, int ID, ContentType contentType)
        {
            var contentStore = _contentLockStores[contentType];
            return contentStore.ReleaseLock(connection, ID);
        }

        private static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

    }
}
