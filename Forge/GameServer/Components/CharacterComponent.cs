using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Components;
using Inspire.Shared.Models;
using Lidgren.Network;

namespace GameServer.Components
{
    public class CharacterComponent : Component 
    {

        public NetConnection Connection { get; set; }

        public CharacterComponent(NetConnection connection)
        {
            Connection = connection;
        }
    }
}
