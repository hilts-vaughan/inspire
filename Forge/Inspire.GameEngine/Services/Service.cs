using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Inspire.GameEngine.ScreenManager;

namespace Inspire.GameEngine.Services
{
    /// <summary>
    /// A service that is used to manipulate and manage entities within a world
    /// </summary>
    public abstract class Service
    {

        /// <summary>
        /// Draws this particular service 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates this particular service
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Used to process user input for a particular sub system
        /// </summary>
        /// <param name="inputState"></param>
        public abstract void HandleInput(InputState inputState);

        /// <summary>
        /// The parent container
        /// </summary>
        public ServiceContainer ServiceManager { get; set; }

        /// <summary>
        /// The content manager for this service - useful for loading in assets.
        /// </summary>
        public ContentManager ContentManager { get; set; }

        /// <summary>
        /// This method is called when the service is added and laoded succesfully. 
        /// </summary>
        public abstract void Initialize();


    }
}
