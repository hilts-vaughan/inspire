using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlastersShared.Network;
using BlastersShared.Network.Packets;
using Lidgren.Network;

namespace BlastersShared.Network.Packets
{

    /// <summary>
    /// This packet is completely artifical, it just is sent to the packet service
    /// when a player has disconnected so the world state can be updated accordingly. 
    /// </summary>
    public class SPlayerDisconnect : Packet
    {

        #region Overrides of Packet

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            return new SPlayerDisconnect();
        }

      


        #endregion
    }
}
