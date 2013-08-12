using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public ObservableCollection<MapSimulator> MapSimulators { get; set; }

        private Dictionary<NetConnection, Entity> _characterLookup = new Dictionary<NetConnection, Entity>();

        public Entity GetEntityFromConnection(NetConnection connection)
        {
            if (!_characterLookup.ContainsKey(connection))
                return null;
            return _characterLookup[connection];
        }



        public ServerServiceContainer()
        {
            Characters = new List<Entity>();
            MapSimulators = new ObservableCollection<MapSimulator>();

            MapSimulators.CollectionChanged += MapSimulators_CollectionChanged;

        }

        void MapSimulators_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MapSimulator mapSimulator in e.NewItems)
                    mapSimulator.CharacterAdded += CharacterAdded;


                return;
            }


            throw new Exception("Removing a map from the simulation is not allowed.");
        }

        private Dictionary<Entity, MapSimulator> _characterSimulatorLookup = new Dictionary<Entity, MapSimulator>();

        public MapSimulator GetSimulatorForCharacter(NetConnection connect)
        {
            return _characterSimulatorLookup[_characterLookup[connect]];
        }

        private void CharacterAdded(object sender, Entity entity)
        {
            if (_characterSimulatorLookup.ContainsKey(entity))
                _characterSimulatorLookup.Remove(entity);
            _characterSimulatorLookup.Add(entity, sender as MapSimulator);

            // Grab character key
            var connection = entity.GetComponent<CharacterComponent>().Connection;

            if(!_characterLookup.ContainsKey(connection))
                _characterLookup.Add(connection, entity);
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
