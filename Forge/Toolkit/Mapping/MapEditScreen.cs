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
        private readonly GameMap _gameMap;
        public TileMapRenderer _renderer;

        public MapEditScreen(GameMap gameMap)
        {
            _gameMap = gameMap;
        }

        public override void LoadContent()
        {
            _renderer = new TileMapRenderer(this, _gameMap);
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
