using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlastersShared.Network
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
