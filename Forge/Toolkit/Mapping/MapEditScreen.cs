using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.GameEngine.Components;
using Inspire.GameEngine.Services.Rendering;
using Inspire.GameEngine.States;
using Inspire.Shared.Components;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping
{
    public class MapEditScreen : ServiceEntityScreen
    {
        public GameMap GameMap;
        public TileMapRenderer _renderer;

        public MapEditScreen(GameMap gameMap)
        {
            GameMap = gameMap;
        }

        public override void LoadContent()
        {
            _renderer = new TileMapRenderer(this, GameMap);
            base.LoadContent();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            // Render it out
            if (_renderer != null)
                _renderer.Draw(spriteBatch);


            base.Draw(gameTime);
        }

    }
}
