﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;

namespace Toolkit.Mapping.Actions.Clipboard
{
    public class ClipboardCutMapAction : IMapAction
    {
        public string ActionName
        {
            get { return "Cut layer";  }
        }


        private Rectangle _cutSection;
        private int _layer;
        private int[,] _oldData;

        public ClipboardCutMapAction(Rectangle cutSection, int layer, int[,] oldData)
        {
            _cutSection = cutSection;
            _layer = layer;
            _oldData = oldData;
        }

        public void Execute(GameMap gameMap)
        {

            Rectangle selected = _cutSection;
            var x = selected.X / 32;
            var y = selected.Y / 32;

            // We need to loop over the width of the editor
            for (int w = 0; w < selected.Width / 32; w++)
            {
                for (int h = 0; h < selected.Height / 32; h++)
                {
                    gameMap.Layers[_layer].MapTiles[x + w][y + h].TileId = -1;

                }
            }


        }

     

        public void UnExecute(GameMap gameMap)
        {

            Rectangle selected = _cutSection;
            var x = selected.X / 32;
            var y = selected.Y / 32;

            // We need to loop over the width of the editor
            for (int w = 0; w < selected.Width / 32; w++)
            {
                for (int h = 0; h < selected.Height / 32; h++)
                {
                    gameMap.Layers[_layer].MapTiles[x + w][y + h].TileId = _oldData[w,h];

                }
            }

        }
    }
}
