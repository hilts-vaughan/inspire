using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Game;
using GameServer.Services;
using Inspire.Shared.Components;
using Inspire.Shared.Models;
using Inspire.Shared.Models.Map;
using Inspire.Shared.Service;
using Lidgren.Network;

namespace GameServer
{
    /// <summary>
    /// The service container has state that all services can access
    /// </summary>
    public class ServerServiceContainer : ServiceContainer
    {

        /// <summary>
        /// A list of characters that are logged in
        /// </summary>
        public List<Entity> Characters { get; set; }
        public List<MapSimulator> MapSimulators { get; set; }

        private Dictionary<NetConnection, Entity> _characterLookup = new Dictionary<NetConnection, Entity>();

        public Entity GetEntityFromConnection(NetConnection connection)
        {
            if (!_characterLookup.ContainsKey(connection))
                return null;
            return _characterLookup[connection];
        }

        public void AddCharacter(Character character, NetConnection conneciton)
        {
            Characters.Add(null);
            _characterLookup.Add(conneciton, null);
        }

        public ServerServiceContainer()
        {
            Characters = new List<Entity>();
            MapSimulators = new List<MapSimulator>();
        }

        public override void RegisterService(Service service)
        {
            base.RegisterService(service);
        }

        public void SetMapSimulator(Service service, MapSimulator simulator)
        {

            // Check if it's a map service
            var mapService = service as IMapService;

            if (mapService != null)
            {
                mapService.MapSimulator = simulator;
                mapService.AfterMapSetup();
            }
        }

    }
}
