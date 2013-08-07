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
        private int _previousID;

        List<Node> _affectedNodes = new List<Node>();

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

        public FloodToolAction(int x, int y, int layer, Rectangle selectedTiles)
            : base(x, y, layer, selectedTiles)
        {
        }

        public new string ActionName
        {
            get { return "Flood tool"; }
        }

        public new void Execute(GameMap gameMap)
        {
            _map = gameMap;
            _previousID = gameMap.Layers[Layer].MapTiles[X][Y].TileId;

            var gX = SelectedTiles.X / 32;
            var gY = SelectedTiles.Y / 32;

            var tY = (gY) * MapEditorGlobals.CurrentActiveTexture.Width / 32;
            var tX = gX;
            var tileID = tY + tX;

            // Restore the state, rewind
            for (int x = 0; x < gameMap.Layers[0].Width; x++)
            {
                for (int y = 0; y < gameMap.Layers[0].Height; y++)
                {
                    Node node = new Node(x, y, gameMap.Layers[Layer].MapTiles[x][y].TileId);
                    _affectedNodes.Add(node);
                }
            }


            FloodFill(new Node(X, Y, gameMap.Layers[Layer].MapTiles[X][Y].TileId), gameMap.Layers[Layer].MapTiles[X][Y].TileId, tileID, Layer);
        }

        public new void UnExecute(GameMap gameMap)
        {

            foreach (var node in _affectedNodes)
            {
                gameMap.Layers[Layer].MapTiles[node.X][node.Y].TileId = node.ID;

            }
        }


        private void FloodFill(Node start, int targetID, int replacementID, int layer)
        {

            if (targetID == replacementID)
                return;

            var _nodes = new Stack<Node>();
            _nodes.Push(start);

            while (_nodes.Count > 0)
            {
                var n = _nodes.Pop();

                if (n.ID == targetID)
                {
                    var no = new Node(n.X, n.Y, _map.Layers[layer].MapTiles[n.X][n.Y].TileId);

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
