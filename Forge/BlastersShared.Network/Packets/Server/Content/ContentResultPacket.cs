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
        public bool Locked { get; set; }

        public ContentResultPacket(object contentObject, bool locked)
        {
            ContentObject = contentObject;
            Locked = locked;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(Locked);

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


            if (!locked)
            {
                var length = incomingMessage.ReadInt32();
                var bytes = incomingMessage.ReadBytes(length);

                o = SerializationHelper.ByteArrayToObject(bytes);
            }

            var packet = new ContentResultPacket(o, locked);

            return packet;
        }


    }
}
