using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using GameServer.Game;
using GameServer.Network;
using Inspire.Network.Packets.Server.Entity;
using Inspire.Shared.Components;

namespace GameServer.Services
{
    public class EntitySyncService : ServerService, IMapService
    {
        public override void PeformUpdate()
        {

        }

        public override void Setup()
        {

        }

        public MapSimulator MapSimulator { get; set; }

        public void AfterMapSetup()
        {
            // Hook into the additional and removal packets
            MapSimulator.EntityCollection.Entities.CollectionChanged += EntityChangeEvent;
        }

        private void EntityChangeEvent(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    foreach (Entity o in notifyCollectionChangedEventArgs.NewItems)
                    {

                        // Send the new addition to everyone
                        SendNetworkAddition(o);

                        var characterComponent = o.GetComponent<CharacterComponent>();
                        if (characterComponent != null)
                        {
                            // If this is a player, send them all current entities, too
                            foreach (var entity in MapSimulator.EntityCollection.Entities)
                            {
                                if (entity == o)
                                    continue;

                                var packet = new EntityAddPacket(entity);
                                ClientNetworkManager.Instance.SendPacket(packet, characterComponent.Connection);
                            }
                        }




                        Logger.Instance.Log(Level.Debug, "Entity with ID " + o.ID + " was added to a map.");
                    }



                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var o in notifyCollectionChangedEventArgs.NewItems)
                        SendNetworkSubtraction((Entity)o);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    throw new Exception("Clearing the entity list is not supported. This is a dangerous operation.");
            }
        }

        private void SendNetworkSubtraction(Entity entity)
        {
            var packet = new EntityRemovePacket(entity.ID);

            // Get all interested clients
            var characters = MapSimulator.EntityCollection.Filter<CharacterComponent>();

            // Send to all interested parties
            foreach (var character in characters.Entities)
            {
                var characterComponent = character.GetComponent<CharacterComponent>();
                ClientNetworkManager.Instance.SendPacket(packet, characterComponent.Connection);
            }
        }


        private void SendNetworkAddition(Entity entity)
        {
            var packet = new EntityAddPacket(entity);

            // Get all interested clients
            var characters = MapSimulator.EntityCollection.Filter<CharacterComponent>();

            // Send to all interested parties
            foreach (var character in characters.Entities)
            {
                var characterComponent = character.GetComponent<CharacterComponent>();
                ClientNetworkManager.Instance.SendPacket(packet, characterComponent.Connection);
            }

        }



    }
}
