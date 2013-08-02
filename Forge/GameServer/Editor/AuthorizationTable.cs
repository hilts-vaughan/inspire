using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace GameServer.Editor
{
    /// <summary>
    /// An authorization table is a thin wrapper that allows a service to determine whether a given connection is authenticated or not.
    /// It also delegates locking of content resources 
    /// </summary>
    public class AuthorizationTable
    {
        // A list of users authorized to perform
        List<NetConnection> _authenticatedUsers = new List<NetConnection>(); 

        public bool IsConnectionAuthorized(NetConnection connection)
        {
            return _authenticatedUsers.Contains(connection);
        }


        /// <summary>
        /// Adds a user to the list of users who are authenticated
        /// </summary>
        /// <param name="connection"></param>
        public void AuthenticateUser(NetConnection connection)
        {         
            _authenticatedUsers.Add(connection);
        }

        /// <summary>
        /// Deauthenticates a given user
        /// </summary>
        /// <param name="connection"></param>
        public void DeauthenticateUser(NetConnection connection)
        {
            _authenticatedUsers.Remove(connection);
        }




    }
}
