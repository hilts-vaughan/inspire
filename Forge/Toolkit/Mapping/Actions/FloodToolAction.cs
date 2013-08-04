using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions
{
    public class FloodToolAction : IMapAction
    {
        public string ActionName { get; private set; }

        private GameMap _map;

        public void Execute(GameMap gameMap, int x, int y, int layer)
        {
            _map = gameMap;

            var gX = MapEditorGlobals.RectangleSelectedTiles.X / 32;
            var gY = MapEditorGlobals.RectangleSelectedTiles.Y / 32 ;

            var tY = (gY) * MapEditorGlobals.CurrentActiveTexture.Width / 32;
            var tX = gX;
            var tileID = tY + tX;

            FloodFill(new Node(x, y, gameMap.Layers[layer].MapTiles[x][y].TileId), gameMap.Layers[layer].MapTiles[x][y].TileId, tileID, layer);

        }

        struct Node
        {
            public int X;
            public int Y;
            public int ID;

            public Node(int x, int y, int id)
            {
                X = x;
                Y = y;
                ID = id;
            }
        }


        //       Flood-fill (node, target-color, replacement-color):
        //1. Set Q to the empty queue.
        //2. Add node to the end of Q.
        //4. While Q is not empty: 
        //5.     Set n equal to the last element of Q.
        //7.     Remove last element from Q.
        //8.     If the color of n is equal to target-color:
        //9.         Set the color of n to replacement-color.
        //10.        Add west node to end of Q.
        //11.        Add east node to end of Q.
        //12.        Add north node to end of Q.
        //13.        Add south node to end of Q.
        //14. Return.

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
