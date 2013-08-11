using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using Inspire.Shared;
using Inspire.Shared.Models.Map;
using Lidgren.Network;

namespace Inspire.Network.Packets.Server
{
    public class SendMapPacket : Packet
    {

        public GameMap Map { get; set; }


        public SendMapPacket(GameMap map)
        {
            Map = map;
        }


        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            var buffer = SerializationHelper.ObjectToByteArray(Map);
            netOutgoingMessage.Write(buffer.Length);
            netOutgoingMessage.Write(buffer);


            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var result =
                (GameMap) SerializationHelper.ByteArrayToObject(incomingMessage.ReadBytes(incomingMessage.ReadInt32()));
            var packet = new SendMapPacket(result);
            return packet;
        }

    }
}
