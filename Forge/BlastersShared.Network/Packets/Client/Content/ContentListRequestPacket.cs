using Inspire.Network.Filters;
using Inspire.Shared.Models.Enums;
using Lidgren.Network;

namespace Inspire.Network.Packets.Client.Content
{
    /// <summary>
    /// A packet used to request authentcation to the lobby server
    /// </summary>
    [AuthorizedPacketFilter]
    public class ContentListRequestPacket : Packet
    {


        public ContentType ContentType { get; set; }


        public ContentListRequestPacket(ContentType contentType)
        {
            ContentType = contentType;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write((byte) ContentType);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var type = (ContentType) incomingMessage.ReadByte();
            var packet = new ContentListRequestPacket(type);
            return packet;
        }


    }
}
