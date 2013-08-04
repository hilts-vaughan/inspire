using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions
{
    public class PencilAction : IMapAction
    {
        public string ActionName { get; private set; }
        public void Execute(GameMap gameMap, int x, int y, int layer)
        {
            // Get vertical portion
            var curTexture = MapEditorGlobals.CurrentActiveTexture;
            var global = MapEditorGlobals.RectangleSelectedTiles;

            // We need to loop over the width of the editor
            for (int w = 0; w < MapEditorGlobals.RectangleSelectedTiles.Width / 32; w++)
            {
                for (int h = 0; h < MapEditorGlobals.RectangleSelectedTiles.Height / 32; h++)
                {

                    var gX = MapEditorGlobals.RectangleSelectedTiles.X / 32 + w;
                    var gY = MapEditorGlobals.RectangleSelectedTiles.Y / 32 + h;

                    var tY = (gY) * curTexture.Width / 32;
                    var tX = gX;
                    var tileID = tY + tX;

                    gameMap.Layers[layer].MapTiles[x + w][y + h].TileId = tileID;
                }
            }
        }
    }
}
