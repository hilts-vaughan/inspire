using Inspire.Shared;
using Lidgren.Network;

namespace Inspire.Network.Packets.Server.Entity
{
    /// <summary>
    /// This packet is sent from the app server to a user to indicate that an entity is being spawned
    /// </summary>
    public class EntityAddPacket : Packet
    {

        public EntityAddPacket(Shared.Components.Entity entity)
        {
            Entity = entity;
        }


        public Shared.Components.Entity Entity { get; set; }



        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            // Get byte data
            var buffer = SerializationHelper.ObjectToByteArray(Entity);
            netOutgoingMessage.Write(buffer.Length);
            netOutgoingMessage.Write(buffer);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var o = (Shared.Components.Entity)SerializationHelper.ByteArrayToObject(incomingMessage.ReadBytes(incomingMessage.ReadInt32()));
            var packet = new EntityAddPacket(o);
            return packet;
        }

    }
}
