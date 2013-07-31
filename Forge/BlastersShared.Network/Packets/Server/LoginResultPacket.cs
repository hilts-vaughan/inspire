using Lidgren.Network;

namespace Inspire.Network.Packets.Server
{
    /// <summary>
    /// A packet containing a list of sessions information - pushed down to the clients so they can make decisssions
    /// on what games they may want to join.
    /// </summary>
    public class LoginResultPacket : Packet
    {
        /// <summary>
        /// An enumeration representing all the possible results
        /// </summary>
        public enum LoginResult
        {
            Succesful,
            Failed
        }

        /// <summary>
        /// The result of the request; whether it was successful or not
        /// </summary>
        public LoginResult Result
        {
            get;
            set;

        }

        public LoginResultPacket(LoginResult result)
        {
            Result = result;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write((byte)Result);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var result = (LoginResult)incomingMessage.ReadByte();
            var packet = new LoginResultPacket(result);
            return packet;
        }


    }
}
