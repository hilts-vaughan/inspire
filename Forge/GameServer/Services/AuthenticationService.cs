using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using GameServer.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Service;

namespace LobbyServer.Services
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
            return true;
        }

        public override void PeformUpdate()
        {

        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }


    }
}
