using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;

namespace Toolkit.Mapping.Actions
{
    public class FloodToolAction : GenericToolAction, IMapAction
    {

        private GameMap _map;

        struct Node
        {
            public readonly int X;
            public readonly int Y;
            public readonly int ID;

            public Node(int x, int y, int id)
            {
                X = x;
                Y = y;
                ID = id;
            }
        }

        public FloodToolAction(int x, int y, int layer, Rectangle selectedTiles) : base(x, y, layer, selectedTiles)
        {
        }

        public new string ActionName
        {
           get { return "Flood tool"; }
        }

        public new void Execute(GameMap gameMap)
        {
            _map = gameMap;

            var gX = MapEditorGlobals.RectangleSelectedTiles.X / 32;
            var gY = MapEditorGlobals.RectangleSelectedTiles.Y / 32;

            var tY = (gY) * MapEditorGlobals.CurrentActiveTexture.Width / 32;
            var tX = gX;
            var tileID = tY + tX;

            FloodFill(new Node(X, Y, gameMap.Layers[Layer].MapTiles[X][Y].TileId), gameMap.Layers[Layer].MapTiles[X][Y].TileId, tileID, Layer);
        }

        public new void UnExecute(GameMap gameMap)
        {
            throw new NotImplementedException();
        }


        private void FloodFill(Node start, int targetID, int replacementID, int layer)
        {

            if(targetID == replacementID)
                return;            

            var _nodes = new Stack<Node>();
            _nodes.Push(start);

            while (_nodes.Count > 0)
            {
                var n = _nodes.Pop();

                if (n.ID == targetID)
                {
                    _map.Layers[layer].MapTiles[n.X][n.Y].TileId = replacementID;


                    if (n.X != _map.Layers[layer].Width - 1)
                        _nodes.Push(new Node(n.X + 1, n.Y, _map.Layers[layer].MapTiles[n.X + 1][n.Y].TileId));

                    if (n.X != 0)
                        _nodes.Push(new Node(n.X - 1, n.Y, _map.Layers[layer].MapTiles[n.X - 1][n.Y].TileId));

                    if (n.Y != _map.Layers[layer].Height - 1)
                        _nodes.Push(new Node(n.X, n.Y + 1, _map.Layers[layer].MapTiles[n.X][n.Y + 1].TileId));

                    if (n.Y != 0)
                        _nodes.Push(new Node(n.X, n.Y - 1, _map.Layers[layer].MapTiles[n.X][n.Y - 1].TileId));

                }

            }




        }


    }
}
