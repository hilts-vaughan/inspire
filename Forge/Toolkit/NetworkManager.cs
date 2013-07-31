using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Inspire.Network;
using Lidgren.Network;
using LobbyServer.Network;

namespace Toolkit.Network
{
    /// <summary>
    /// Creates and manages networking server that listens for incoming connections and handles the incoming data. 
    /// </summary>
    public class NetworkManager
    {
        private static NetworkManager _instance;

        //The packet service used to process information
        PacketService _packetService = new PacketService();

        public static NetworkManager Instance
        {
            get { if (_instance == null) _instance = new NetworkManager(); return _instance; }
        }

        private NetClient _client;

        public NetworkManager()
        {
            var config = new NetPeerConfiguration("MyExampleName");

            _client = new NetClient(config);
            _client.Start();


            //99.236.248.133 
            _client.Connect(File.ReadAllText(ProjectSettings.Instance.Location + "\\ip.txt"), 2562);
        }

        public NetPeerStatistics Statistics
        {
            get { return _client.Statistics; }
        }

        /// <summary>
        /// Gets the average latency to the server
        /// </summary>
        public float AverageLatency
        {
            get
            {
                return _client.ServerConnection.AverageRoundtripTime;
            }
        }

        /// <summary>
        /// Creates an emtpty message.
        /// </summary>
        /// <returns></returns>
        public NetOutgoingMessage CreateMessage()
        {
            return _client.CreateMessage();
        }

        public void ConnectTo(string host, int port)
        {
            _client.Disconnect("Switching zones... bye-bye.");

            //Give it some time to breathe.
                Thread.Sleep(100);


            _client.Connect(host, port);

            Thread.Sleep(100);

        }

        public void ChangeLatency(float latency)
        {
            _client.Configuration.SimulatedMinimumLatency += latency;
        }

        public void SendPacket(Packet packet)
        {
            NetOutgoingMessage message = _client.CreateMessage();
            _client.SendMessage(packet.ToNetBuffer(ref message), NetDeliveryMethod.ReliableOrdered);
        }


       
        /// <summary>
        /// Updates the NetworkManager and checks for new messages, and acts accordingly. 
        /// </summary>
        public void Update()
        {
            NetIncomingMessage incomingMessage;


            while ((incomingMessage = _client.ReadMessage()) != null)
            {
                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(incomingMessage.ReadString());
                        break;
                    case NetIncomingMessageType.Data:

                        //Read the packet ID
                        int packetID = incomingMessage.ReadInt32();

                        
                        break;
                    default:
                        Console.WriteLine("Unhandled type: " + incomingMessage.MessageType);
                        break;
                }
                _client.Recycle(incomingMessage);

            }

        }




    }
}
