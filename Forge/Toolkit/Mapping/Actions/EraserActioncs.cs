using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions
{
    public class EraserAction : IMapAction
    {
        public string ActionName
        {
            get { return "Eraser"; }
        }

        public void Execute(GameMap gameMap, int x, int y, int layer)
        {
            gameMap.Layers[layer].MapTiles[x][y].TileId = -1;
        }

 
    }
}
