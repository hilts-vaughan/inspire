using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace BlastersShared.Network.Packets
{
    /// <summary>
    /// Represents a bare minimum packet that was sent over the wire
    /// </summary>
    public abstract class Packet
    {
        /// <summary>
        /// Creates a packet of this type from the specified <see cref="NetIncomingMessage"/>
        /// </summary>
        /// <param name="incomingMessage">The message to create this packet from</param>
        /// <returns></returns>
        public static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {


            return null;
        }

        /// <summary>
        /// Converts the packet into an outgoing packet
        /// </summary>
        /// <returns>Returns the specified IPacket as a packet</returns>
        public virtual NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            //TODO: Find a way to write the packet header given the HashCode of the class name...
            netOutgoingMessage.Write(GlobalPacketMap.PacketCache[GetType()]);

            return null;
        }



        /// <summary>
        /// The sender of this given constructed packet, mainly used on the server side
        /// </summary>
        public NetConnection Sender { get; set; }


    }
}
