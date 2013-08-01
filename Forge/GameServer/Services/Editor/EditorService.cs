using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using GameServer.Models;
using GameServer.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Client.Content;
using Inspire.Network.Packets.Server;
using Inspire.Network.Packets.Server.Content;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;
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
            PacketService.RegisterPacket<ContentListRequestPacket>(Handler);
        }

        private void Handler(ContentListRequestPacket contentListRequestPacket)
        {
            var type = contentListRequestPacket.ContentType;
            List<EditorTemplateEntry> editorTemplateEntries;

            switch (type)
            {
                case ContentType.Item:
                    editorTemplateEntries = GetAllItems();
                    break;

                default:
                    Logger.Instance.Log(Level.Warn, "The client has requested a resource with an unknown identifier.");
                    return;
            }


            if (editorTemplateEntries != null)
            {
                var packet = new ContentListResultPacket(editorTemplateEntries);
                ClientNetworkManager.Instance.SendPacket(packet, contentListRequestPacket.Sender);
            }

        }

        private List<EditorTemplateEntry> GetAllItems()
        {
            var entries = new List<EditorTemplateEntry>();
            var context = new ServerContext();

            foreach (var itemTemplate in context.ItemTemplates)
                entries.Add(new EditorTemplateEntry(itemTemplate.ID, itemTemplate.Name));
            return entries;
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
