using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Network;
using Inspire.Network.Packets.All;
using Inspire.Shared.Components;
using Inspire.Shared.Models.Enums;

namespace GameServer.Services.Chat
{
    public class ChatService : ServerService
    {
        public ChatService()
        {
            PacketService.RegisterPacket<ChatPacket>(HandleChatMessage);
        }

        private void HandleChatMessage(ChatPacket chatPacket)
        {
            var container = (ServerServiceContainer)ServiceContainer;

            // Grab the sender
            var sender = container.GetEntityFromConnection(chatPacket.Sender);
            var name = sender.GetComponent<NameComponent>().Name;

            // Send packet to anyone who cares
            switch (chatPacket.ChatChannel)
            {
                case ChatChannel.Map:

                    var simulator = container.GetSimulatorForCharacter(chatPacket.Sender);

                    // Filter for other players in that map
                    var playersNearby = simulator.EntityCollection.Filter<CharacterComponent>();
                    chatPacket.Message = sender.GetComponent<NameComponent>().Name + ": " + chatPacket.Message;

                    foreach (var connection in playersNearby.Entities.Select(player => player.GetComponent<CharacterComponent>().Connection))
                        SendMessage(chatPacket, connection);

                    break;
                default:
                    throw new Exception("Unsupported chat type was sent by the client.");
            }


        }

        private void SendMessage(ChatPacket chatPacket, Lidgren.Network.NetConnection connection)
        {
            var packet = new ChatPacket(chatPacket.Message, chatPacket.ChatChannel);
            ClientNetworkManager.Instance.SendPacket(packet, connection);
        }


        public override void PeformUpdate()
        {

        }

        public override void Setup()
        {

        }
    }
}
