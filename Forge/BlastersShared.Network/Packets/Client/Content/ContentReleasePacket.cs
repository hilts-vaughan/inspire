using Inspire.Network.Filters;
using Inspire.Shared.Models.Enums;
using Lidgren.Network;

namespace Inspire.Network.Packets.Client.Content
{
    /// <summary>
    /// A packet used to request authentcation to the lobby server
    /// </summary>
    [AuthorizedPacketFilter]
    public class ContentReleasePacket : Packet
    {
        public ContentReleasePacket(ContentType contentType, int id)
        {
            ContentType = contentType;
            ID = id;
        }

        public ContentType ContentType { get; set; }
        public int ID { get; set; }



        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(ID);
            netOutgoingMessage.Write((byte)ContentType);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var id = incomingMessage.ReadInt32();
            var type = (ContentType)incomingMessage.ReadByte();
            var packet = new ContentReleasePacket(type, id);
            return packet;
        }


    }
}
