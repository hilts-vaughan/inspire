using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;

namespace Toolkit.Mapping.Actions
{
    public class PencilAction : GenericToolAction, IMapAction
    {
        List<int> _previousTiles = new List<int>(); 

        public PencilAction(int x, int y, int layer, Rectangle selectedTiles) : base(x, y, layer, selectedTiles)
        {

        }

        public new string ActionName
        {
            get { return "Pencil tool"; }
        }
        
        

        public new void Execute(GameMap gameMap)
        {


            _previousTiles.Clear();

            // Get vertical portion
            var curTexture = MapEditorGlobals.CurrentActiveTexture;

            // We need to loop over the width of the editor
            for (int w = 0; w < SelectedTiles.Width / 32; w++)
            {
                for (int h = 0; h < SelectedTiles.Height / 32; h++)
                {

                    var gX = SelectedTiles.X / 32 + w;
                    var gY = SelectedTiles.Y / 32 + h;

                    var tY = (gY) * curTexture.Width / 32;
                    var tX = gX;
                    var tileID = tY + tX;

                    _previousTiles.Add(gameMap.Layers[Layer].MapTiles[X + w][Y + h].TileId);
                    gameMap.Layers[Layer].MapTiles[X + w][Y + h].TileId = tileID;
                }
            }
        }

        public new void UnExecute(GameMap gameMap)
        {
            // Get vertical portion
            var curTexture = MapEditorGlobals.CurrentActiveTexture;

            // We need to loop over the width of the editor
            for (int w = 0; w < SelectedTiles.Width / 32; w++)
            {
                for (int h = 0; h < SelectedTiles.Height / 32; h++)
                {

                    var gX = SelectedTiles.X / 32 + w;
                    var gY = SelectedTiles.Y / 32 + h;

                    var tY = (gY) * curTexture.Width / 32;
                    var tX = gX;
                    var tileID = tY + tX;

                    gameMap.Layers[Layer].MapTiles[X + w][Y + h].TileId = _previousTiles.First();
                    _previousTiles.Remove(_previousTiles.First());
                }
            }

        }
    }
}
