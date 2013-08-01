using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using GameServer.Models;
using GameServer.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Models.Enums;
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
            var type = contentRequestPacket.ContentType;
            object o;

            switch (type)
            {
                case ContentType.Item:
                    o = GetItemByID(contentRequestPacket.ID);
                    break;

                default:
                    Logger.Instance.Log(Level.Warn, "The client has requested a resource with an unknown identifier.");
                    return;
            }


            // Make sure we actually recieved something
            if (o != null)
            {
                var packet = new ContentResultPacket(o);
                ClientNetworkManager.Instance.SendPacket(packet, contentRequestPacket.Sender);
            }


        }

        private object GetItemByID(int id)
        {
            var context = new ServerContext();
            var item = context.ItemTemplates.FirstOrDefault(x => x.ID == id);
            return item;
        }


        public override void PeformUpdate()
        {

        }
    }
}
