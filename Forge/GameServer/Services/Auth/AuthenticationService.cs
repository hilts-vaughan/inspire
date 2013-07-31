using System.Linq;
using System.Security.Cryptography;
using System.Text;
using GameServer.Models;
using GameServer.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Crypto;
using Inspire.Shared.Service;

namespace GameServer.Services.Auth
{
    /// <summary>
    /// Provides authentication related utilities to handle login requests. 
    /// This service writes to the user dictionary. 
    /// </summary>
    public class AuthenticationService : Service
    {

        public AuthenticationService()
        {

            RegisterNetworkCallbacks();
        }

        void RegisterNetworkCallbacks()
        {
            // Bind out network events
            PacketService.RegisterPacket<LoginRequestPacket>(ProcessLoginRequest);
        }

        private void ProcessLoginRequest(LoginRequestPacket obj)
        {
            var username = obj.Username;
            var password = obj.Password;

            if (AreCredentialsValid(username, password))
            {
               // var user = AddUser(obj, username);

                //Logger.Instance.Log(Level.Info, user.Name + " has joined the lobby.");

           
                var packet = new LoginResultPacket(LoginResultPacket.LoginResult.Succesful);
                ClientNetworkManager.Instance.SendPacket(packet, obj.Sender);

            }

            
            else
            {
                // Reject the user if they aren't able to authenticate

                var packet = new LoginResultPacket(LoginResultPacket.LoginResult.Failed);
                ClientNetworkManager.Instance.SendPacket(packet, obj.Sender);
            }
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
            var account = context.Accounts.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());

            if (account == null)
                return false;

            return HashHelper.CalculateSha512Hash(account.Password) == password;
        }

        public override void PeformUpdate()
        {

        }


    }
}
