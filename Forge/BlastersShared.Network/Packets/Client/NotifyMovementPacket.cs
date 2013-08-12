using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Inspire.Network.Packets.Client
{
    public class NotifyMovementPacket : Packet
    {

        /// <summary>
        /// The velocity this entity is moving in at the current snapshot
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// The location of this entity at the current snapshot
        /// </summary>
        public Vector2 Location { get; set; }

        public ulong EntityID { get; set; }

        public NotifyMovementPacket(Vector2 velocity, Vector2 location, ulong entityID)
        {
            Velocity = velocity;
            Location = location;
            EntityID = entityID;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(Velocity.X);
            netOutgoingMessage.Write(Velocity.Y);

            netOutgoingMessage.Write(Location.X);
            netOutgoingMessage.Write(Location.Y);

            netOutgoingMessage.Write(EntityID);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {

            // Read values back in
            var velocity = new Vector2(incomingMessage.ReadFloat(), incomingMessage.ReadFloat());
            var location = new Vector2(incomingMessage.ReadFloat(), incomingMessage.ReadFloat());
            var id = incomingMessage.ReadUInt64();

            var packet = new NotifyMovementPacket(velocity, location, id);
            return packet;
        }

    }
}
