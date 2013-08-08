using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;

namespace Toolkit.Mapping.Actions
{
    public class EraserAction : GenericToolAction, IMapAction
    {
        private int _previousID;

        public EraserAction(int x, int y, int layer, Rectangle selectedTiles) : base(x, y, layer, selectedTiles)
        {
        }

        public new string ActionName
        {
            get { return "Eraser tool"; }
        }

        public new void UnExecute(GameMap gameMap)
        {
            if (X >= gameMap.Layers[0].Width || Y >= gameMap.Layers[0].Height)
                return;
            gameMap.Layers[Layer].MapTiles[X][Y].TileId = _previousID;
        }

        public new void Execute(GameMap gameMap)
        {
            if (X  >= gameMap.Layers[0].Width || Y  >= gameMap.Layers[0].Height)
                return;

            _previousID = gameMap.Layers[Layer].MapTiles[X][Y].TileId;
            gameMap.Layers[Layer].MapTiles[X][Y].TileId = -1;
        }

 
    }
}
