using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Services;
using Inspire.GameEngine;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.GameEngine.Services;
using Inspire.GameEngine.Services.Rendering;
using Inspire.Network.Packets.Server;
using Microsoft.Xna.Framework;

namespace GameClient.Screens
{
    public class GameplayScreen : GameScreen
    {

        private TileMapRenderer _tileMapRenderer;
        private ServiceContainer _serviceContainer;

        public GameplayScreen()
        {
            Camera2D = new Camera2D(new Vector2(1024, 768), 10000, 10000, 1f);            
            PacketService.RegisterPacket<SendMapPacket>(RecievedMap);
        }

  

        private void RecievedMap(SendMapPacket obj)
        {
            // Setup 
            Camera2D.SetWorldSize(obj.Map.Layers[0].Width, obj.Map.Layers[0].Height);
            _tileMapRenderer = new TileMapRenderer(this, obj.Map);        
        }

        public override void LoadContent()
        {

            // Create container
            _serviceContainer = new ServiceContainer(ScreenManager.ContentManager, ScreenManager.GraphicsDevice);
            _serviceContainer.Camera = Camera2D;

            // Prepare services we need
            RegisterServices();

            UiManager.Load("");

            base.LoadContent();
        }

        private void RegisterServices()
        {
            var spriteRender = new SpriteRenderingService();
            var entitySync = new EntitySyncService();

            _serviceContainer.AddService(spriteRender);
            _serviceContainer.AddService(entitySync);

        }


        public override void HandleInput(InputState input)
        {
            _serviceContainer.UpdateInput(input);
            base.HandleInput(input);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _serviceContainer.UpdateService(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            // Draw the map
            if (_tileMapRenderer != null)
                _tileMapRenderer.Draw(spriteBatch);

            _serviceContainer.Draw(spriteBatch);

            base.Draw(gameTime);
        }

    }
}
