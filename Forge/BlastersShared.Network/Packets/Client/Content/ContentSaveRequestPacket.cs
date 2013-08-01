using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Models.Enums;
using Lidgren.Network;

namespace Inspire.Network.Packets.Client.Content
{
    public class ContentSaveRequestPacket : Packet
    {
         /// <summary>
        /// An object that has content shoved within it
        /// </summary>
        public object ContentObject { get; set; }
        public ContentType ContentType { get; set; }

        public ContentSaveRequestPacket(object contentObject, ContentType contentType)
        {
            ContentObject = contentObject;
            ContentType = contentType;
        }


        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            var bytes = SerializationHelper.ObjectToByteArray(ContentObject);

            netOutgoingMessage.Write(bytes.Length);
            netOutgoingMessage.Write(bytes);
            netOutgoingMessage.Write((byte) ContentType);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var length = incomingMessage.ReadInt32();
            var bytes = incomingMessage.ReadBytes(length);
            var type = (ContentType) incomingMessage.ReadByte();

            var o = SerializationHelper.ByteArrayToObject(bytes);
            var packet = new ContentSaveRequestPacket(o, type);

            return packet;
        }

    }
}
