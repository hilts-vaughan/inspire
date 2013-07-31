using Lidgren.Network;

namespace Inspire.Network.Packets
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
