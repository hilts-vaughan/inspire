using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.GameEngine.ScreenManager;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Inspire.GameEngine.Services.Rendering
{
    /// <summary>
    /// The tile map renderer is used by the the game states that require it - not being an entity it's special, however.
    /// </summary>
    public class TileMapRenderer
    {

        // A well kept tileset texture which is used for rendering the current map
        public Texture2D _tilesetTexture;

        /// <summary>
        /// The tilemap renderer can attach itself to a parent screen to recieve things it needs
        /// </summary>
        public GameScreen ParentScreen { get; set; }

        private GameMap _gameMap;

        public TileMapRenderer(GameScreen parentScreen, GameMap gameMap)
        {
            ParentScreen = parentScreen;
            _gameMap = gameMap;

            // Load up our texture if we need to
            _tilesetTexture = TextureLoader.GetTexture(@"Levels\castle.png", parentScreen.ScreenManager.GraphicsDevice);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            foreach (var layer in _gameMap.Layers)
            {
                for (int i = 0; i < layer.MapTiles.GetLength(0); i++)
                {
                    for (int j = 0; j < layer.MapTiles[0].GetLength(0); j++)
                    {

                        if(layer.MapTiles[i][j].TileId < 0)
                            continue;

                        var y = layer.MapTiles[i][j].TileId /(_tilesetTexture.Width/32);
                        var x = layer.MapTiles[i][j].TileId%(_tilesetTexture.Width/32);

                       spriteBatch.Draw(_tilesetTexture, new Vector2(i * 32, j * 32), new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                    }
                }

            }

            spriteBatch.End();

        }

    }
}
