using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Editor;
using GameServer.Models;
using GameServer.Network;
using Inspire.Network.Packets.Client;
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
            
            using (var context = new ServerContext())
            {
                
            }

            _authorizationTable.AuthenticateUser(editorLoginRequestPacket.Sender);
        }

        public override void PeformUpdate()
        {
            
        }
    }
}
