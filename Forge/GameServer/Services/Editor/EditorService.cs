using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using Lidgren.Network;

namespace GameServer.Services.Editor
{
    /// <summary>
    /// The editor service allows developers to edit data over the wire and send it back for processing.
    /// </summary>  
    public class EditorService : ServerService
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
                            case ContentType.Map:
                                var map = context.MapTemplates.Attach(contentSaveRequestPacket.ContentObject as MapTemplate);
                                context.Entry(map).State = EntityState.Modified;
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
                    editorTemplateEntries = GetAllItems(contentListRequestPacket.Sender);
                    break;
                case ContentType.Skill:
                    editorTemplateEntries = GetAllSkills(contentListRequestPacket.Sender);
                    break;
                case ContentType.Map:
                    editorTemplateEntries = GetAllMaps(contentListRequestPacket.Sender);
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

        private List<EditorTemplateEntry> GetAllMaps(NetConnection connection)
        {
            var entries = new List<EditorTemplateEntry>();
            var context = new ServerContext();

            foreach (var itemTemplate in context.MapTemplates)
            {
                var locked = _contentLockManager.AnyoneHasLock(connection, itemTemplate.Id, ContentType.Map);
                entries.Add(new EditorTemplateEntry(itemTemplate.Id, itemTemplate.Name, itemTemplate.VirtualCategory,
                                                    ContentType.Map, locked));
            }
            return entries;
        }

        private List<EditorTemplateEntry> GetAllSkills(NetConnection connection)
        {
            var entries = new List<EditorTemplateEntry>();
            var context = new ServerContext();


            foreach (var skilltemplate in context.SkillTemplates)
            {
                var locked = _contentLockManager.AnyoneHasLock(connection, skilltemplate.Id, ContentType.Skill);
                entries.Add(new EditorTemplateEntry(skilltemplate.Id, skilltemplate.Name, skilltemplate.VirtualCategory,
                                                    ContentType.Skill, locked));
            }
            return entries;
        }

        private List<EditorTemplateEntry> GetAllItems(NetConnection connection)
        {
            var entries = new List<EditorTemplateEntry>();
            var context = new ServerContext();

            foreach (var itemTemplate in context.ItemTemplates)
            {
                var locked = _contentLockManager.AnyoneHasLock(connection, itemTemplate.Id, ContentType.Item);
                entries.Add(new EditorTemplateEntry(itemTemplate.Id, itemTemplate.Name, itemTemplate.VirtualCategory,
                                                    ContentType.Item, locked));
            }
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
                    case ContentType.Map:
                        o = GetMapByID(contentRequestPacket.ID);
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

        private object GetMapByID(int id)
        {
            var context = new ServerContext();
            var map = context.MapTemplates.FirstOrDefault(x => x.Id == id);
            return map;
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
