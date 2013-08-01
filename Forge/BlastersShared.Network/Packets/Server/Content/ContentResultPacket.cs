using BlastersShared;
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

        public ContentResultPacket(object contentObject)
        {
            ContentObject = contentObject;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            var bytes = SerializationHelper.ObjectToByteArray(ContentObject);

            netOutgoingMessage.Write(bytes.Length);
            netOutgoingMessage.Write(bytes);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var length = incomingMessage.ReadInt32();
            var bytes = incomingMessage.ReadBytes(length);

            var o = SerializationHelper.ByteArrayToObject(bytes);
            var packet = new ContentResultPacket(o);

            return packet;
        }


    }
}
