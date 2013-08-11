using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.Services.Rendering;

namespace GameClient.Screens
{
    public class GameplayScreen : GameScreen
    {

        private TileMapRenderer _tileMapRenderer;


        public override void LoadContent()
        {
            base.LoadContent();
        }


        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            // Draw the map
            if (_tileMapRenderer != null)
                _tileMapRenderer.Draw(spriteBatch);

            base.Draw(gameTime);
        }

    }
}
