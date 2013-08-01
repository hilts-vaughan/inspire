using System;
using System.Net;
using BlastersShared;
using Inspire.Network;
using Inspire.Network.Packets;
using Lidgren.Network;

namespace GameServer.Network
{
    /// <summary>
    /// Creates and manages networking server that listens for incoming connections and handles the incoming data. 
    /// </summary>
    public class ClientNetworkManager
    {
        private static ClientNetworkManager _instance;

        public static ClientNetworkManager Instance
        {
            get { return _instance ?? (_instance = new ClientNetworkManager()); }
        }

        private readonly NetServer _server;
        public readonly PacketService PacketService = new PacketService();
        private readonly PacketProcessor _packetProcessor = new PacketProcessor();


        public ClientNetworkManager()
        {
            var config = new NetPeerConfiguration("Inspire") { Port = 8787, ConnectionTimeout = 30 };

            //Simulate bad network conditions optionally
            /*
            #if DEBUG
                config.SimulatedMinimumLatency = 0.3f;
                config.SimulatedRandomLatency = 0.1f;
            #endif
            */

            //Disconnect users after 10 seconds


            _server = new NetServer(config);
            _server.Start();
        }

        IPAddress getIP()
        {
            string localIP = "?";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }

            return IPAddress.Parse(localIP);
        }

        /// <summary>
        /// Creates an emtpty message.
        /// </summary>
        /// <returns></returns>
        public NetOutgoingMessage CreateMessage()
        {
            return _server.CreateMessage();
        }

        public void SendPacket(Packet packet, NetConnection connection)
        {
            NetOutgoingMessage message = _server.CreateMessage();
            _server.SendMessage(packet.ToNetBuffer(ref message), connection, NetDeliveryMethod.ReliableOrdered);
        }

        public NetPeerStatistics Statistics
        {
            get { return _server.Statistics; }
        }


        public delegate void ClientDisconnectedEvent(object sender, ClientDisconnectedEventArgs ca); 


        public class ClientDisconnectedEventArgs : EventArgs
        {
            public NetConnection Connection { get; set; }

            public ClientDisconnectedEventArgs(NetConnection connection)
            {
                Connection = connection;
            }
        }

        public event ClientDisconnectedEvent ClientDisconnected;

        public void InvokeClientDisconnected(ClientDisconnectedEventArgs ca)
        {
            ClientDisconnectedEvent handler = ClientDisconnected;
            if (handler != null) handler(this, ca);
        }


        /// <summary>
        /// Updates the NetworkManager and checks for new messages, and acts accordingly. 
        /// </summary>
        public void Update()
        {
            NetIncomingMessage incomingMessage;

            while ((incomingMessage = _server.ReadMessage()) != null)
            {
                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(incomingMessage.ReadString());

                        ////If a player disconnected, signal a disconnect packet to the server
                        //if (netStatus == NetConnectionStatus.Disconnected)
                        //{
                        //    var dcPacket = new SPlayerDisconnect();
                        //    dcPacket.Sender = incomingMessage.SenderConnection;

                        //    PacketService.ProcessReceivedPacket(dcPacket);

                        //}
                        break;

                    case NetIncomingMessageType.Data:


                  

                        //Read the packet ID
                        int packetID = incomingMessage.ReadInt32();

                        //Alert all other sub systems of the presence of this packet
                        _packetProcessor.ProcessPacket(packetID, incomingMessage);
                        break;

                    default:
                        break;
                }

                _server.Recycle(incomingMessage);
            }

        }
    }
}
