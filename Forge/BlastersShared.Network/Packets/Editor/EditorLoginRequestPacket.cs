﻿using Lidgren.Network;

namespace Inspire.Network.Packets.Client
{
    /// <summary>
    /// A packet used to request authentcation to the lobby server
    /// </summary>
    public class EditorLoginRequestPacket : Packet
    {

        public string Username { get; set; }
        public string Password { get; set; }


        public EditorLoginRequestPacket(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override NetOutgoingMessage ToNetBuffer(ref NetOutgoingMessage netOutgoingMessage)
        {
            base.ToNetBuffer(ref netOutgoingMessage);

            netOutgoingMessage.Write(Username);
            netOutgoingMessage.Write(Password);

            return netOutgoingMessage;
        }


        public new static Packet FromNetBuffer(NetIncomingMessage incomingMessage)
        {
            var packet = new EditorLoginRequestPacket(incomingMessage.ReadString(), incomingMessage.ReadString());
            return packet;
        }


    }
}
