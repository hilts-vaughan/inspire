using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Network;
using Inspire.Network.Packets.Client;
using Inspire.Shared.Service;

namespace GameServer.Services.Editor
{
    /// <summary>
    /// The editor service allows developers to edit data over the wire and send it back for processing.
    /// </summary>  
    public class EditorService : Service
    {

        public EditorService()
        {
            PacketService.RegisterPacket<ContentRequestPacket>(Handler);   
        }

        private void Handler(ContentRequestPacket contentRequestPacket)
        {
            //TODO: Check if the user is authorized to request this resource


        }


        public override void PeformUpdate()
        {
            
        }
    }
}
