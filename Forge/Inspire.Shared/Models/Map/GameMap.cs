using System.Collections.Generic;
using BlastersShared;
using Inspire.Shared.Models.Templates;

namespace Inspire.Shared.Models.Map
{
    /// <summary>
    /// A game map contains data about the actual map - these are usually created from <see cref="MapTemplate"/>s
    /// </summary>
    public class GameMap
    {

        public GameMap()
        {
            Layers = new List<MapLayer>();
            
            // Create some base layers
            for(int i = 0; i < 3; i++)
                Layers.Add(new MapLayer());

        }

        public static GameMap FromTemplate(MapTemplate template)
        {
            return (GameMap) SerializationHelper.ByteArrayToObject(template.BinaryData);
        }


        /// <summary>
        /// The name of this given map
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of layers on this particular map
        /// </summary>
        public List<MapLayer> Layers { get; set; }

        /// <summary>
        /// The ID of the tileset that is being used on this particular map
        /// </summary>
        public int TilesetID { get; set; }

    }



    public class MapLayer
    {

        public int Width
        {
            get { return MapTiles.GetLength(0); }
        }

        public int Height
        {
            get { return MapTiles[0].GetLength(0); }
        }

        public string Name { get; set; }
        public bool Visible { get; set; }

        public MapLayer()
        {

            Visible = true;

            Name = "New Layer";

            // The default is 100x100
            MapTiles = new MapTile[100][];

            for (int x = 0; x < MapTiles.GetLength(0); x++ )
            {
                MapTiles[x] = new MapTile[100];

                for(int y = 0; y < MapTiles[0].GetLength(0); y++ )
                {
                    MapTiles[x][y] = new MapTile();
                }
            }

        }


        public MapTile[][] MapTiles { get; set; }
   

    }

    /// <summary>
    /// A tile that contains some meta-information
    /// </summary>
    public class MapTile
    {

        public MapTile()
        {
            TileId = -1;
        }

        /// <summary>
        /// The associated tile id
        /// </summary>
        public int TileId { get; set; }

    }


}
