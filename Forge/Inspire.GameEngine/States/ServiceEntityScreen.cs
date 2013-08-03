using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.Services;
using Microsoft.Xna.Framework;

namespace Inspire.GameEngine.States
{
    /// <summary>
    /// This class contains the base implementation of a service container and the like being setup
    /// It allows for various editors and other screens to quickly get entities going in a working fashion
    /// </summary>
    public abstract class ServiceEntityScreen : GameEngine.ScreenManager.GameScreen
    {

        public ServiceContainer ServiceContainer { get; set; }

        protected ServiceEntityScreen()
        {
          
        }

        public override void LoadContent()
        {
            ServiceContainer = new ServiceContainer(ScreenManager.ContentManager, ScreenManager.GraphicsDevice);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {

            ServiceContainer.Draw(ScreenManager.SpriteBatch);
            base.Draw(gameTime);
        }

        public override void HandleInput(InputState input)
        {
            ServiceContainer.UpdateInput(input);
            base.HandleInput(input);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            ServiceContainer.UpdateService(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

    }
}
