using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlastersShared;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;
using Lidgren.Network;

namespace Inspire.Network.Packets.Server.Content
{
    public class ContentListResultPacket : Packet
    {

        /// <summary>
        /// An object that has content shoved within it
        /// </summary>
        public List<EditorTemplateEntry> EditorTemplateEntries { get; set; }
        public ContentType ContentType { get; set; }

        public ContentListResultPacket(List<EditorTemplateEntry> editorTemplateEntries, ContentType contentType)
        {
            EditorTemplateEntries = editorTemplateEntries;
            ContentType = contentType;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);


            netOutgoingMessage.Write((byte) ContentType);

            var bytes = SerializationHelper.ObjectToByteArray(EditorTemplateEntries);            

            netOutgoingMessage.Write(bytes.Length);
            netOutgoingMessage.Write(bytes);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var type = (ContentType) incomingMessage.ReadByte();
            var length = incomingMessage.ReadInt32();
            var bytes = incomingMessage.ReadBytes(length);

            var o = (List<EditorTemplateEntry>) SerializationHelper.ByteArrayToObject(bytes);
            var packet = new ContentListResultPacket(o, type);

            return packet;
        }


    }
}
