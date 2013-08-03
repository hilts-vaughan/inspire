using BlastersShared;
using Inspire.Shared.Models.Enums;
using Lidgren.Network;

namespace Inspire.Network.Packets.Server
{
    /// <summary>
    /// A packet containing a list of sessions information - pushed down to the clients so they can make decisssions
    /// on what games they may want to join.
    /// </summary>
    public class ContentResultPacket : Packet
    {

        /// <summary>
        /// An object that has content shoved within it
        /// </summary>
        public object ContentObject { get; set; }
        public bool Locked { get; set; }
        public ContentType  ContentType { get; set; }

        public ContentResultPacket(object contentObject, bool locked, ContentType contentType)
        {
            ContentObject = contentObject;
            Locked = locked;
            ContentType = contentType;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(Locked);
            netOutgoingMessage.Write((byte)ContentType);

            if (!Locked)
            {
                var bytes = SerializationHelper.ObjectToByteArray(ContentObject);
                netOutgoingMessage.Write(bytes.Length);
                netOutgoingMessage.Write(bytes);
            }

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {

            object o = null;
            var locked = incomingMessage.ReadBoolean();
            var type = (ContentType) incomingMessage.ReadByte();

            if (!locked)
            {
                var length = incomingMessage.ReadInt32();
                var bytes = incomingMessage.ReadBytes(length);

                o = SerializationHelper.ByteArrayToObject(bytes);
            }

            var packet = new ContentResultPacket(o, locked, type);

            return packet;
        }


    }
}
