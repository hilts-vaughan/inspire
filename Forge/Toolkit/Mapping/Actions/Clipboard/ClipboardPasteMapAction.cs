using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;

namespace Toolkit.Mapping.Actions.Clipboard
{
    public class ClipboardPasteMapAction : IMapAction
    {

        
        private Rectangle _cutSection;
        private int _layer;
        private int[,] _oldData;
        private int[,] _newData;

        public ClipboardPasteMapAction(Rectangle cutSection, int layer, int[,] newData)
        {
            _cutSection = cutSection;
            _layer = layer;
            _newData = newData;
        }

        public string ActionName { get; private set; }
        
        public void Execute(GameMap gameMap)
        {
            Rectangle selected = _cutSection;
            var x = selected.X / 32;
            var y = selected.Y / 32;

            _oldData = new int[_newData.GetLength(0),_newData.GetLength(1)];

            // We need to loop over the width of the editor
            for (int w = 0; w < _newData.GetLength(0); w++)
            {
                for (int h = 0; h < _newData.GetLength(1); h++)
                {
                    _oldData[w, h] = gameMap.Layers[_layer].MapTiles[x + w][y + h].TileId;
                    gameMap.Layers[_layer].MapTiles[x + w][y + h].TileId = _newData[w, h];

                }
            }
    
        }

        public void UnExecute(GameMap gameMap)
        {
                  Rectangle selected = _cutSection;
            var x = selected.X / 32;
            var y = selected.Y / 32;


            // We need to loop over the width of the editor
            for (int w = 0; w < _newData.GetLength(0); w++)
            {
                for (int h = 0; h < _newData.GetLength(1); h++)
                {
                    gameMap.Layers[_layer].MapTiles[x + w][y + h].TileId = _oldData[w, h];

                }
            }

        }




    }
}
