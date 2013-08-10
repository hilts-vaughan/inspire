using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Screens
{
    /// <summary>
    /// A login screen is used to collect authorization credentials
    /// </summary>
    public class LoginScreen : GameScreen
    {
        private Texture2D _backdrop;

        public override void LoadContent()
        {
            // Load all the textures we need
            _backdrop = TextureLoader.GetTexture("bg_login.png", ScreenManager.GraphicsDevice);

            UiManager.Load(@"Content\UI\login\index.html");


            base.LoadContent();
        }


        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {


            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(_backdrop, new Vector2(0, 0), Color.White );
            spriteBatch.End();

            base.Draw(gameTime);
     
        }


    }
}
