using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping
{
    public class GameMapSnapshot
    {

        /// <summary>
        /// The map at this current snapshot
        /// </summary>
        public GameMap Map { get; set; }

        /// <summary>
        /// The action that was performed to create this snapshot
        /// </summary>
        public IMapAction Action { get; set; }

    }
}
