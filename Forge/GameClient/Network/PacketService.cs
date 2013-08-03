﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Inspire.Network;

namespace Inspire.GameEngine.ScreenManager.Network
{
    public class PacketService
    {
        private static readonly ConcurrentDictionary<Type, Action<Packet>> Handlers = new ConcurrentDictionary<Type, Action<Packet>>(new Dictionary<Type, Action<Packet>>());

        public static void RegisterPacket<T>(Action<T> handler)
            where T : Packet
        {
            Handlers[typeof(T)] = packet => handler((T)packet);
        }

        public void ProcessReceivedPacket(Packet packet)
        {
            Action<Packet> handler;
            if (!Handlers.TryGetValue(packet.GetType(), out handler))
            {
                //Theres no need to crash, perhaps we just aren't listening for that kind of packet right now
                //if, that's thats the case just apply the 'I don't give a fuck' attitude towards the packet
                return;
            }

            handler(packet);
        }
    }
}