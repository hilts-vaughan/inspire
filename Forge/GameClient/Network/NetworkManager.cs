using System;
using System.IO;
using System.Net.Mime;
using System.Threading;
using Inspire.Network;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Inspire.GameEngine.ScreenManager.Network
{
    /// <summary>
    /// Creates and manages networking server that listens for incoming connections and handles the incoming data. 
    /// </summary>
    public class NetworkManager
    {
        private static NetworkManager _instance;

        //Create a packet processor for servicing packets
        private readonly PacketProcessor _processor = new PacketProcessor();

        public static NetworkManager Instance
        {
            get { return _instance ?? (_instance = new NetworkManager()); }
        }

        private readonly NetClient _client;

        public NetworkManager()
        {
            var config = new NetPeerConfiguration("Inspire");
            Random random = new Random();
            


           config.SimulatedMinimumLatency = (float)MathHelper.Clamp((float)random.NextDouble(), 0, 0.95f); //(float) random.NextDouble();
           config.SimulatedMinimumLatency = (float)MathHelper.Clamp((float)random.NextDouble(), 0, 0.05f); //(float) random.NextDouble();
            config.SimulatedRandomLatency = 0.05f;

            _client = new NetClient(config);

            _client.Start();


        }


        public NetConnectionStatus Status
        {
            get { return _client.ConnectionStatus;  }
        }

        public void Connect()
        {
            _client.Connect(File.ReadAllText(Environment.CurrentDirectory + "\\ip.txt"), 8787);
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
                if (_client.ServerConnection != null)
                    return _client.ServerConnection.AverageRoundtripTime;
                    return 0f;
            }
        }

        public float RoundtripTime
        {
            get { return _client.ServerConnection.AverageRoundtripTime; }

        }

        public void Disconnect()
        {
            _client.Disconnect("Quitting on will.");
        }

        /// <summary>
        /// Creates an emtpty message
        /// </summary>
        /// <returns></returns>
        public NetOutgoingMessage CreateMessage()
        {
            return _client.CreateMessage();
        }

        public void ConnectTo(string host, int port)
        {

            //Give it some time to breathe
            Thread.Sleep(100);

            _client.Connect(host, port);

            Thread.Sleep(100);
        }

        public void ChangeLatency(float latency)
        {

        }

        public void SendPacket(Packet packet)
        {
            NetOutgoingMessage message = _client.CreateMessage();
            _client.SendMessage(packet.ToNetBuffer(ref message), NetDeliveryMethod.ReliableOrdered);

        }

        public class DisconnectArgs : EventArgs
        {
            public string Reason { get; set; }

            public DisconnectArgs(string reason)
            {
                Reason = reason;
            }
        }

        public event EventHandler<DisconnectArgs> ClientDisconnected;

        public void InvokeClientDisconnected(DisconnectArgs e)
        {
            EventHandler<DisconnectArgs> handler = ClientDisconnected;
            if (handler != null) handler(this, e);
        }


        /// <summary>
        /// Updates the NetworkManager and checks for new messages, and acts accordingly
        /// </summary>
        public void Update()
        {
            NetIncomingMessage incomingMessage;

            while ((incomingMessage = _client.ReadMessage()) != null)
            {
                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        break;

                    case NetIncomingMessageType.WarningMessage:
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus type = (NetConnectionStatus)incomingMessage.ReadByte();
                        string message = incomingMessage.ReadString();



                        if (type == NetConnectionStatus.Disconnected)
                        {                       
                            InvokeClientDisconnected(new DisconnectArgs(message));
                        }
                        break;

                    case NetIncomingMessageType.ErrorMessage:
                        break;

                    case NetIncomingMessageType.Data:
                        //Read the packet ID
                        int packetID = incomingMessage.ReadInt32();

                        //Packet is sent to the processor to fire off events
                        _processor.ProcessPacket(packetID, incomingMessage);
                        break;

                    default:
                        Console.WriteLine("Unhandled type: " + incomingMessage.MessageType + '.');
                        break;
                }

                _client.Recycle(incomingMessage);
            }
        }
    }
}