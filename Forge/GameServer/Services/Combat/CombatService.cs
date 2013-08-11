using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Game;

namespace GameServer.Services.Combat
{
    public class CombatService : ServerService, IMapService
    {
        public override void PeformUpdate()
        {
            
        }

        public override void Setup()
        {

        }

        public MapSimulator MapSimulator { get; set; }
    }
}
