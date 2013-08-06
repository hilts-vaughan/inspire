using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;

namespace Toolkit.Mapping.Actions
{
    /// <summary>
    /// A generic tool action is for things like the flood bucket, pencil and other things 
    /// </summary>
    public class GenericToolAction : IMapAction
    {
        protected int X;
        protected int Y;
        protected int Layer;
        protected Rectangle SelectedTiles;

        public GenericToolAction(int x, int y, int layer, Rectangle selectedTiles)
        {
            X = x;
            Y = y;
            Layer = layer;
            SelectedTiles = selectedTiles;
        }

        public string ActionName { get; private set; }
        public void Execute(GameMap gameMap)
        {
 
        }

        public void UnExecute(GameMap gameMap)
        {
       
        }
    }
}
