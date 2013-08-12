using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Network;
using GameServer.Services;
using Inspire.Network.Packets.Client;
using Lidgren.Network;

namespace GameServer.Game
{
    /// <summary>
    /// A map packet proxy takes various incoming packets and delgates them to approriate services
    /// </summary>
    public class MapPacketProxy
    {

        public ServerServiceContainer ServiceContainer { get; set; }

        public MapPacketProxy(ServerServiceContainer serviceContainer)
        {
            ServiceContainer = serviceContainer;

            RegisterCallbacks();
        }

        private void RegisterCallbacks()
        {
            PacketService.RegisterPacket<NotifyMovementPacket>(HandleMovement);
        }

        private void HandleMovement(NotifyMovementPacket notifyMovementPacket)
        {
            var simulator = GetSimulator(notifyMovementPacket.Sender);
            simulator.ServerServiceContainer.GetService<MovementService>().MovementRecieved(notifyMovementPacket);
        }

        private MapSimulator GetSimulator(NetConnection connection)
        {
            return ServiceContainer.GetSimulatorForCharacter(connection);
        }
    }
}
