using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Components;
using Inspire.Shared.Models.Map;

namespace Inspire.GameEngine.Components
{
    /// <summary>
    /// A game map component is a component which contains a given zone map
    /// </summary>
    public class GameMapComponent : Component
    {
        public GameMap GameMap { get; set; }
    }
}
