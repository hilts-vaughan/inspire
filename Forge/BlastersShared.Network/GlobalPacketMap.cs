using System;
using System.Collections.Generic;

namespace Inspire.Network
{
    public static class GlobalPacketMap
    {
        private static Dictionary<Type, int> _packetCache;
        public static Dictionary<Type, int> PacketCache
        {
            get { return _packetCache; }
            set { _packetCache = value; }
        }
    }
}
