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

        public NotifyMovementPacket(Vector2  velocity, Vector2 location)
        {
            Velocity = velocity;
            Location = location;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(Velocity.X);
            netOutgoingMessage.Write(Velocity.Y);

            netOutgoingMessage.Write(Location.X);
            netOutgoingMessage.Write(Location.Y);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {

            // Read values back in
            var velocity = new Vector2(incomingMessage.ReadFloat(), incomingMessage.ReadFloat());
            var location = new Vector2(incomingMessage.ReadFloat(), incomingMessage.ReadFloat());


            var packet = new NotifyMovementPacket(velocity, location);
            return packet;
        }

    }
}
