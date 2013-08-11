using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Game;

namespace GameServer.Services
{
    public interface IMapService
    {
        /// <summary>
        /// The parent map simulator
        /// </summary>
        MapSimulator MapSimulator { get; set; }

        void AfterMapSetup();
    }
}
