using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Components;
using GameServer.Game;
using GameServer.Network;
using Inspire.Network.Packets.Client;
using Inspire.Shared.Components;
using Lidgren.Network;

namespace GameServer.Services
{
    [MapService]
    public class MovementService : ServerService, IMapService
    {
        public MovementService()
        {
            PacketService.RegisterPacket<NotifyMovementPacket>(MovementRecieved);
        }

        private void MovementRecieved(NotifyMovementPacket notifyMovementPacket)
        {
            var user = ServiceContainer.GetEntityFromConnection(notifyMovementPacket.Sender);
            var transform = user.GetComponent<TransformComponent>();

            transform.LastLocalPosition = transform.LocalPosition;
            transform.LocalPosition = notifyMovementPacket.Location;
            transform.ServerPosition = notifyMovementPacket.Location;

            transform.Velocity = notifyMovementPacket.Velocity;

            // Get nearby players that might be interested
            var characters = MapSimulator.EntityCollection.Filter<CharacterComponent>();

            // Alert them one by one
            foreach (var character in characters.Entities.ToList())
            {
                if(character == user)
                    continue;                
                SyncToNearby(character.GetComponent<CharacterComponent>().Connection, transform);
            }

        }

        private void SyncToNearby(NetConnection sendTo, TransformComponent transformComponent)
        {
            var packet = new NotifyMovementPacket(transformComponent.Velocity, transformComponent.LocalPosition);
            ClientNetworkManager.Instance.SendPacket(packet, sendTo);
        }

        public override void PeformUpdate()
        {
            var transformEntities = MapSimulator.EntityCollection.Filter<TransformComponent>();
        }

        public override void Setup()
        {

        }

        public MapSimulator MapSimulator { get; set; }
        
        public void AfterMapSetup()
        {
                
        }


    }
}
