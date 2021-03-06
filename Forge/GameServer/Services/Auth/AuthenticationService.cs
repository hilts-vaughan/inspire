﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BlastersShared;
using GameServer.Game;
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
    public class AuthenticationService : ServerService
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

                Logger.Instance.Log(Level.Info, username + " has succesfully authenticated");
                
                var packet = new LoginResultPacket(LoginResultPacket.LoginResult.Succesful);
                ClientNetworkManager.Instance.SendPacket(packet, obj.Sender);


                // Grab that account
                using (var context = new ServerContext())
                {
                    var account = context.Accounts.FirstOrDefault(x => x.Username.ToLower() == obj.Username);
                    var character = context.Characters.FirstOrDefault(x => x.AccountId == account.AccountId);

                    if(character == null)
                        throw new Exception("A character could not be found under this slot. This should never happen.");

                    // Introduce the entity into the simulation
                    var entity = EntityFactory.CreateCharacter(character, obj.Sender);
                    var mapSimulator = ((ServerServiceContainer) ServiceContainer).MapSimulators[character.MapId];
                    mapSimulator.AddEntity(entity);

                }

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

            return account.Password == password;
        }

        public override void PeformUpdate()
        {

        }

        public override void Setup()
        {
            
        }
    }
}
