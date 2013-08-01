using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Inspire.Network.Packets.Server.Content
{
    public class ContentSaveResultPacket : Packet
    {
     
        public RequestResult RequestResult { get; set; }

        public ContentSaveResultPacket(RequestResult requestResult)
        {
            RequestResult = requestResult;
        }


        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write((byte)RequestResult);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var type = (RequestResult)incomingMessage.ReadByte();
            var packet = new ContentSaveResultPacket(type);
            return packet;
        }


    }
}
