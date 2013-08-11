using Inspire.Shared.Models.Enums;
using Lidgren.Network;

namespace Inspire.Network.Packets.All
{
    /// <summary>
    /// A generic chat packet that is used for communication purposes. 
    /// </summary>
    public class ChatPacket : Packet
    {


        /// <summary>
        /// The message being sent out
        /// </summary>
        public string Message { get; set; }

        public ChatChannel ChatChannel { get; set; }

        public ChatPacket(string message, ChatChannel chatChannel)
        {
            Message = message;
            ChatChannel = chatChannel;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(Message);
            netOutgoingMessage.Write((byte) ChatChannel);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var message = incomingMessage.ReadString();
            var channel = (ChatChannel) incomingMessage.ReadByte();
            return new ChatPacket(message, channel);
        }

      



    }
}
