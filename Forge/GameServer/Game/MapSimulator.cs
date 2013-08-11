using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Network;
using GameServer.Services;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Components;
using Inspire.Shared.Models.Map;
using Inspire.Shared.Models.Templates;
using Inspire.Shared.Service;
using Lidgren.Network;

namespace GameServer.Game
{
    /// <summary>
    /// A map simulator is responsible for a single map and registering all the services it needs
    /// </summary>
    public class MapSimulator
    {
        /// <summary>
        /// This is the current game map
        /// </summary>
        public GameMap GameMap { get; private set; }

        // A service container allows services to be hooked up
        public readonly ServerServiceContainer ServerServiceContainer = new ServerServiceContainer();
        private Dictionary<NetConnection, Entity> _characterMap = new Dictionary<NetConnection, Entity>();

        public EntityCollection EntityCollection { get; private set; }

        public MapSimulator(MapTemplate template)
        {
            // We assign the map inside here
            GameMap = GameMap.FromTemplate(template);
            EntityCollection = new EntityCollection();

            // Register all the services we want here
            RegisterServices();
        }

        public void AddEntity(Entity entity)
        {
            // Add the entity
            EntityCollection.Entities.Add(entity);

            // If it's a player character, make note of that
            var characterComponent = entity.GetComponent<CharacterComponent>();

            if (characterComponent != null)
                AddCharacter(entity, characterComponent.Connection);

        }

        private void AddCharacter(Entity entity, NetConnection connection)
        {
            _characterMap.Add(connection, entity);

            // When we add a character, we should also send map data if required
            SendMapTo(connection);
        }

        private void SendMapTo(NetConnection connection)
        {
            var packet = new SendMapPacket(GameMap);
            ClientNetworkManager.Instance.SendPacket(packet, connection);
        }

        private void RegisterServices()
        {
            var movementService = new MovementService();
            ServerServiceContainer.RegisterService(movementService);
            ServerServiceContainer.SetMapSimulator(movementService, this);





            // Syncing should always be done last
            var entitySync = new EntitySyncService();
            ServerServiceContainer.RegisterService(entitySync);
            ServerServiceContainer.SetMapSimulator(entitySync, this);


        }



    }
}
