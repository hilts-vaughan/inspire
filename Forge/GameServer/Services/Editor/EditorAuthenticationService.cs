using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Editor;
using GameServer.Models;
using GameServer.Network;
using Inspire.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Crypto;
using Inspire.Shared.Service;

namespace GameServer.Services.Editor
{
    /// <summary>
    /// This service is responsible for authenticating users
    /// </summary>
    public class EditorAuthenticationService : Service
    {
        private AuthorizationTable _authorizationTable;

        public EditorAuthenticationService(AuthorizationTable authorizationTable)
        {
            PacketService.RegisterPacket<EditorLoginRequestPacket>(LoginHandler);

            _authorizationTable = authorizationTable;
        }


        private void LoginHandler(EditorLoginRequestPacket editorLoginRequestPacket)
        {
            var username = editorLoginRequestPacket.Username;
            var password = editorLoginRequestPacket.Password;

            var result = LoginResultPacket.LoginResult.Succesful;

            if (AreCredentialsValid(username, password))
                _authorizationTable.AuthenticateUser(editorLoginRequestPacket.Sender);
            else
                result = LoginResultPacket.LoginResult.Failed;

            var packet = new LoginResultPacket(result);
            ClientNetworkManager.Instance.SendPacket(packet, editorLoginRequestPacket.Sender);


        }


        /// <summary>
        /// Determines whether the given username and password are valid to authenticate with.
        /// </summary>
        /// <param name="username">The username to challenge</param>
        /// <param name="password">The password to challenge</param>
        bool AreCredentialsValid(string username, string password)
        {

            // Get our context
            var context = new ServerContext();
            var account = context.Accounts.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.EditorAllowed);

            if (account == null)
                return false;

            return account.Password  == password;
        }

        public override void PeformUpdate()
        {

        }

        public override void Setup()
        {

        }
    }
}
