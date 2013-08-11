using System;
using System.Collections.Generic;
using System.IO;
using Inspire.GameEngine;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.ScreenManager.Components;
using Inspire.GameEngine.ScreenManager.Utilities;
using Inspire.GameEngine.Services;
using Inspire.Shared.Components;
using Inspire.Shared.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Services
{
    /// <summary>
    /// The sprite rendering service is responsible for drawing entities that might have sprites.
    /// </summary>
    public class SpriteRenderingService : Service
    {
        // This is used to look up sprites for drawing. It's cached in memory for ease of use
        readonly Dictionary<string, SpriteDescriptor> _spriteDescriptorLookup = new Dictionary<string, SpriteDescriptor>();
        private SpriteFont _entityFont;
        private float _lastAnimationTimer;

        public Dictionary<string, SpriteDescriptor> SpriteDescriptorLookup
        {
            get { return _spriteDescriptorLookup; }
        }

        public override void Initialize()
        {
            LoadDescriptors();

            // Load fonts
            _entityFont = ContentManager.Load<SpriteFont>(@"Fonts\AnonPro");

            // Listen for when an entity might hav ebeen added on
            ServiceManager.EntityAdded += ServiceManagerOnEntityAdded;
        }

        private void ServiceManagerOnEntityAdded(Entity entity)
        {
            // Attempt to add the sprite component
            AddSpriteComponent(entity);
        }

        private void LoadDescriptors()
        {
            // It's much easier to grab all the descriptors in one go
            // Then, they're all available in memory and there's nothing to worry about
            foreach (var file in Directory.GetFiles(PathUtility.SpriteDescriptorPath, "*.*", SearchOption.AllDirectories))
            {
                var descriptor = SpriteDescriptor.FromFile(file);
                _spriteDescriptorLookup.Add(descriptor.Name, descriptor);
            }

            foreach (var entity in ServiceManager.EntityCollection.Entities)
            {
                AddSpriteComponent(entity);
            }
        }

        private void AddSpriteComponent(Entity entity)
        {
            var skinComponent = entity.GetComponent<SkinComponent>();

            if (skinComponent != null)
            {
                var spriteComponent = new SpriteComponent
                {
                    Texture =
                        TextureLoader.GetTexture(_spriteDescriptorLookup[skinComponent.SpriteDescriptorName].SpritePath,
                                                  ServiceManager.GraphicsDevice),
                    SpriteDescriptor = _spriteDescriptorLookup[skinComponent.SpriteDescriptorName]
                };

                entity.AddComponent(spriteComponent);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, ServiceManager.Camera.GetTransformation());

            var sprites = ServiceManager.EntityCollection.Filter<SpriteComponent>()
                .Filter<TransformComponent>();

            foreach (var entity in sprites.Entities)
            {
                var spriteComponent = entity.GetComponent<SpriteComponent>();
                var nameComponent = entity.GetComponent<NameComponent>();
                var transformComponent = entity.GetComponent<TransformComponent>();

                if (spriteComponent.Texture.Width == 320)
                    continue;

                int animation = (int)transformComponent.DirectionalCache;
                if (entity.HasComponent<CharacterComponent>())
                    animation = 0;

                var skinComponent = entity.GetComponent<SkinComponent>();
                var descriptor = _spriteDescriptorLookup[skinComponent.SpriteDescriptorName];
                var sourceRectangle = new Rectangle(
                    (int)descriptor.FrameSize.X * spriteComponent.AnimationFrame,
                    (int)(descriptor.FrameSize.Y * descriptor.Animations[animation].Row),
                    (int)descriptor.FrameSize.X, (int)descriptor.FrameSize.Y);

                spriteBatch.Draw(spriteComponent.Texture, transformComponent.LocalPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, descriptor.SpriteDepth);

                // If this sprite has a name
                if (nameComponent != null)
                {
                    var font = _entityFont;
                    var size = font.MeasureString(nameComponent.Name);
                    var namePos = transformComponent.LocalPosition;

                    Vector2 pos = namePos - new Vector2(0, 0);

                    pos = pos + new Vector2((int)(transformComponent.Size.X / 2), -20);
                    pos = pos - new Vector2((int)(size.X / 2), 0);

                    pos = new Vector2((float)Math.Round(pos.X), (float)Math.Round(pos.Y));


                    spriteBatch.DrawString(font, nameComponent.Name, pos, Color.White);
                }


            }

            spriteBatch.End();
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            var sprites = ServiceManager.EntityCollection.Filter<SpriteComponent>();

            foreach (var entity in sprites.Entities)
            {

                var spriteComponent = entity.GetComponent<SpriteComponent>();

                spriteComponent.LastFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                _lastAnimationTimer = spriteComponent.LastFrameTime;

                var transformComponent = entity.GetComponent<TransformComponent>();
                var skinComponent = entity.GetComponent<SkinComponent>();
                var descriptor = _spriteDescriptorLookup[skinComponent.SpriteDescriptorName];

                // Total amount of frames
                var frameCount = spriteComponent.Texture.Width / descriptor.FrameSize.X;

                // We don't need to animate if there's only one frame
                if ((int)frameCount == 1)
                    continue;


                // Don't animate if the player isn't moving
                if (transformComponent.Velocity != Vector2.Zero || !entity.HasComponent<CharacterComponent>())
                {
                    // Change animation frame every 1/4 of a second
                    if (_lastAnimationTimer >= spriteComponent.SpriteDescriptor.Animations[0].Speed)
                    {
                        spriteComponent.AnimationFrame++;

                        if (spriteComponent.AnimationFrame > frameCount - 1)
                        {
                            spriteComponent.AnimationFrame = 0;

                            // Check if this was a one shot deal
                            if (entity.HasComponent<OneShotAnimationComponent>())
                                ServiceManager.RemoveEntity(entity);

                        }

                        spriteComponent.LastFrameTime = 0;
                    }
                }
                else
                    // Reset animation frame if they stop moving; default for moving objects
                    spriteComponent.AnimationFrame = 1;

            }
        }

        public override void Update(GameTime gameTime)
        {

            // Update animation frames
            UpdateAnimation(gameTime);

            //throw new NotImplementedException();
        }

        public override void HandleInput(InputState inputState)
        {
            //throw new NotImplementedException();
        }
    }
}
