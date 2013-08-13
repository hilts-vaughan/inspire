using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared;
using Inspire.Shared.Models.Map;
using Lidgren.Network;

namespace Inspire.Network.Packets.Server
{
    public class SendMapPacket : Packet
    {

        public GameMap Map { get; set; }
        public ulong PlayerId { get; set; }

        public SendMapPacket(GameMap map, ulong playerId)
        {
            Map = map;
            PlayerId = playerId;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            var buffer = SerializationHelper.ObjectToByteArray(Map);
            netOutgoingMessage.Write(buffer.Length);
            netOutgoingMessage.Write(buffer);
            netOutgoingMessage.Write(PlayerId);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var result =
                (GameMap) SerializationHelper.ByteArrayToObject(incomingMessage.ReadBytes(incomingMessage.ReadInt32()));
            var id = incomingMessage.ReadUInt64();
            var packet = new SendMapPacket(result, id);
            return packet;
        }

    }
}
