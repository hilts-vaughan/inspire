using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BlastersShared.Network;
using BlastersShared.Network.Packets;
using Lidgren.Network;

namespace LobbyServer.Network
{
    /// <summary>
    /// The packet processor is responsbile for taking an incoming packet and identifying its type
    /// </summary>
    class PacketProcessor
    {

        //We keep a cache of the PacketIDs and their types, this prevents unnecessary iterations and lag.
        //This is especially important server-side!
        Dictionary<int, Type> _cache = new Dictionary<int, Type>();

        //The packet service used to process information
        readonly PacketService _packetService = new PacketService();

        /// <summary>
        /// Alerts the packet service about possible packets that need processing
        /// </summary>
        /// <param name="packetID">The type of packet that is expected</param>
        /// <param name="incomingMessage">The data contained within the packet</param>
        public void ProcessPacket(int packetID, NetIncomingMessage incomingMessage)
        {
            //TODO: Type.InvokeMember(...) is an expensive call. Do monitor this if at all possible.
            //It'll likely be fine, and it adds great design addition. No nasty switches. Send and forget. 

            if (!_cache.ContainsKey(packetID))
                throw new Exception("The given packet type is missing! Was it marked properly and given an attribute?");

            //Take our incoming message and type
            var type = _cache[packetID];

            var val = new object[1];
            val[0] = incomingMessage;

            //Take the given type in the context and create our packet from it
            var returnValue = (Packet)type.InvokeMember("FromNetBuffer", BindingFlags.InvokeMethod, null, type, val);
            returnValue.Sender = incomingMessage.SenderConnection;

            //Automated packet service
            _packetService.ProcessReceivedPacket(returnValue);
        }

        public PacketProcessor()
        {
            //Load up the global packet cache
            GlobalPacketMap.PacketCache = new Dictionary<Type, int>();

            //Get our executing assembly, 'Dream'
            var assembly = Assembly.GetAssembly(typeof(Packet));

            //We're interested in shared packets and the client packets, lets cache em up
            foreach (var type in assembly.GetTypes())
            {
                //If the name starts with "S", it's safe to assume this is OK; along with deriving from 'Packet'
                if (type.IsSubclassOf(typeof(Packet)))
                {

                    //Assign this packet an ID based off the hash-code it has...
                    _cache.Add(type.Name.GetHashCode(), type);
                    GlobalPacketMap.PacketCache.Add(type, type.Name.GetHashCode());
                }


            }

        }


    }
}
