using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Network;
using Inspire.Network.Packets.All;

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
            // Grab the sender
            //var sender = ServiceContainer.GetEntityFromConnection(chatPacket.Sender);
            //var name = sender.GetComponent<CharacterComponent>().CharacterModel.Name;


        }


        public override void PeformUpdate()
        {
  
        }

        public override void Setup()
        {
        
        }
    }
}
