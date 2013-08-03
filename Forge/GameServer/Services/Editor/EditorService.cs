using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using GameServer.Editor;
using GameServer.Editor.ContentLocking;
using GameServer.Models;
using GameServer.Network;
using Inspire.Network;
using Inspire.Network.Packets;
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
        // Some objects that will come in handy
        private AuthorizationTable _authorizationTable = new AuthorizationTable();
        private ContentLockManager _contentLockManager = new ContentLockManager();

        // Services bootstrapped by this one
        private static EditorAuthenticationService _editorAuthenticationService;

        public EditorService()
        {

        }

        private void Handler(ContentSaveRequestPacket contentSaveRequestPacket)
        {
            var result = RequestResult.Succesful;

            var locked = !_contentLockManager.HasLock(contentSaveRequestPacket.Sender, contentSaveRequestPacket.ContentObject.Id,
                                                contentSaveRequestPacket.ContentType);
            if (!locked)
            {
                try
                {

                    // Create our context and use it
                    using (var context = new ServerContext())
                    {
                        switch (contentSaveRequestPacket.ContentType)
                        {
                            case ContentType.Item:
                                var item =
                                    context.ItemTemplates.Attach(contentSaveRequestPacket.ContentObject as ItemTemplate);
                                context.Entry(item).State = EntityState.Modified;
                                context.SaveChanges();
                                break;
                            case ContentType.Skill:
                                var skill =
                                    context.SkillTemplates.Attach(
                                        contentSaveRequestPacket.ContentObject as SkillTemplate);
                                context.Entry(skill).State = EntityState.Modified;
                                context.SaveChanges();
                                break;

                            default:
                                result = RequestResult.Failed;
                                return;
                        }
                    }

                }
                catch (Exception exception)
                {
                    // Log the error and eat it
                    Logger.Instance.Log(Level.Error,
                                        "The content of type " + contentSaveRequestPacket.ContentType +
                                        " failed to save: " + exception);
                    result = RequestResult.Failed;
                }
            }
            else
            {
                // If it's locked, it's considered a failure
                result = RequestResult.Failed;
            }

            // Send the result either way back to the client
            var packet = new ContentSaveResultPacket(result);
            ClientNetworkManager.Instance.SendPacket(packet, contentSaveRequestPacket.Sender);

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
                case ContentType.Skill:
                    editorTemplateEntries = GetAllSkills();
                    break;
                default:
                    Logger.Instance.Log(Level.Warn, "The client has requested a resource with an unknown identifier.");
                    return;
            }


            if (editorTemplateEntries != null)
            {
                var packet = new ContentListResultPacket(editorTemplateEntries, contentListRequestPacket.ContentType);
                ClientNetworkManager.Instance.SendPacket(packet, contentListRequestPacket.Sender);
            }

        }

        private List<EditorTemplateEntry> GetAllSkills()
        {
            var entries = new List<EditorTemplateEntry>();
            var context = new ServerContext();

            foreach (var skilltemplate in context.SkillTemplates)
                entries.Add(new EditorTemplateEntry(skilltemplate.Id, skilltemplate.Name));
            return entries;
        }

        private List<EditorTemplateEntry> GetAllItems()
        {
            var entries = new List<EditorTemplateEntry>();
            var context = new ServerContext();

            foreach (var itemTemplate in context.ItemTemplates)
                entries.Add(new EditorTemplateEntry(itemTemplate.Id, itemTemplate.Name));
            return entries;
        }

        private void Handler(ContentRequestPacket contentRequestPacket)
        {
            var type = contentRequestPacket.ContentType;
            object o = null;
            var locked = !_contentLockManager.TryAcquireLock(contentRequestPacket.Sender, contentRequestPacket.ID,
                                                            contentRequestPacket.ContentType);

            if (!locked)
            {

                switch (type)
                {
                    case ContentType.Item:
                        o = GetItemByID(contentRequestPacket.ID);
                        break;
                    case ContentType.Skill:
                        o = GetSkillByID(contentRequestPacket.ID);
                        break;
                    default:
                        Logger.Instance.Log(Level.Warn,
                                            "The client has requested a resource with an unknown identifier.");
                        return;
                }
            }

            var packet = new ContentResultPacket(o, locked, contentRequestPacket.ContentType);
            ClientNetworkManager.Instance.SendPacket(packet, contentRequestPacket.Sender);



        }

        private object GetItemByID(int id)
        {
            var context = new ServerContext();
            var item = context.ItemTemplates.FirstOrDefault(x => x.Id == id);
            return item;
        }

        private object GetSkillByID(int id)
        {
            var context = new ServerContext();
            var skill = context.SkillTemplates.FirstOrDefault(x => x.Id == id);
            return skill;
        }


        public override void PeformUpdate()
        {

        }

        public override void Setup()
        {
            PacketService.RegisterPacket<ContentRequestPacket>(Handler);
            PacketService.RegisterPacket<ContentListRequestPacket>(Handler);
            PacketService.RegisterPacket<ContentSaveRequestPacket>(Handler);
            PacketService.RegisterPacket<ContentReleasePacket>(HandleRelease);

            // We care about connections disconnecting
            PacketService.RegisterPacket<SPlayerDisconnect>(Handler);

            // The editor module will bootstrap it's own services
            _editorAuthenticationService = new EditorAuthenticationService(_authorizationTable);


            // Set them all up
            ServiceContainer.RegisterService(_editorAuthenticationService);

        }

        private void Handler(SPlayerDisconnect sPlayerDisconnect)
        {
            _contentLockManager.PurgeLocks(sPlayerDisconnect.Sender);
        }   

        private void HandleRelease(ContentReleasePacket contentReleasePacket)
        {
            // The client claims it's done, so relesae the lock if needed
            var success = _contentLockManager.TryReleaseLock(contentReleasePacket.Sender, contentReleasePacket.ID,
                                               contentReleasePacket.ContentType);

            if (!success)
                Logger.Instance.Log(Level.Warn, "A connection attempted to release a resource that didn't belong to them. Ignoring.");
        }

    }
}
