using System.Collections.Generic;
using GameClient.Services.Movement;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine.ScreenManager.Components;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.GameEngine.Services;
using Inspire.Network.Packets.Client;
using Inspire.Shared.Components;
using Inspire.Shared.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace GameClient.Services
{
    /// <summary>
    /// The movement service is response for moving entities along
    /// </summary>
    public class MovementService : Service
    {
        private readonly ulong _idToMonitor;

        private readonly Dictionary<ulong, EntityInterpolator> _entityInterpolators = new Dictionary<ulong, EntityInterpolator>();

        // Timing related info (amount of updates sent per seconds i.e 0.1 is 10FPS )
        const float MovementRate = 0.1f;
        private float _lastReaction;
        private Vector2 lastTransformVector = Vector2.Zero;

        public Dictionary<string, SpriteDescriptor> SpriteDescriptorLookup { get; set; }

        public MovementService(ulong idToMonitor)
        {
            _idToMonitor = idToMonitor;
        }

        public override void Initialize()
        {
            // Hook into networks events
            PacketService.RegisterPacket<NotifyMovementPacket>(MovementRecieved);

            // Query for the players we don't want
            foreach (var entity in ServiceManager.EntityCollection.Entities)
            {
                if (entity.ID == _idToMonitor)
                {
                    continue;
                }

                var transformComponent = entity.GetComponent<TransformComponent>();

                var interpolator = new EntityInterpolator(transformComponent);
                _entityInterpolators.Add(entity.ID, interpolator);
            }
        }

        private void MovementRecieved(NotifyMovementPacket obj)
        {
            Entity player = null;

            foreach (var entity in ServiceManager.EntityCollection.Entities)
            {
                if (entity.ID == obj.EntityID)
                {
                    player = entity;
                }
            }

            if (player != null)
            {
                var transformComponent = player.GetComponent<TransformComponent>();

                transformComponent.Velocity = obj.Velocity;


                if (transformComponent.Velocity.X != transformComponent.Velocity.Y)
                {
                    if (obj.Velocity.X < 0)
                        transformComponent.DirectionalCache = DirectionalCache.Left;

                    if (obj.Velocity.X > 0)
                        transformComponent.DirectionalCache = DirectionalCache.Right;

                    if (obj.Velocity.Y > 0)
                        transformComponent.DirectionalCache = DirectionalCache.Down;

                    if (obj.Velocity.Y < 0)
                        transformComponent.DirectionalCache = DirectionalCache.Up;
                }
            }

            // Retreieve an interpolator
            var interpolator = _entityInterpolators[obj.EntityID];
            interpolator.ResetProgress(obj.Location);
        }

    
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, ServiceManager.Camera.GetTransformation());
            
            foreach (var entity in ServiceManager.EntityCollection.Entities)
            {
                // Local players can be moved automatically, then report their status if needed
                var skinComponent = entity.GetComponent<SkinComponent>();
                // TODO: Make it so we can get the sprite descriptors from the sprite service.
                var spriteDescriptor = entity.GetComponent<SpriteComponent>().SpriteDescriptor;
                var transformComponent = entity.GetComponent<TransformComponent>();

                Rectangle bbox = new Rectangle(
                    (int) (transformComponent.LocalPosition.X + spriteDescriptor.BoundingBox.X),
                    (int) (transformComponent.LocalPosition.Y + spriteDescriptor.BoundingBox.Y),
                    spriteDescriptor.BoundingBox.Width,
                    spriteDescriptor.BoundingBox.Height);
                
                spriteBatch.DrawRectangle(bbox, Color.White, 2f);
            }

           

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ServiceManager.EntityCollection.Entities)
            {
                // Local and remote entities are treated differently
                if (entity.ID == _idToMonitor)
                    ProcessLocalPlayer(entity, gameTime);
                else
                    ProcessRemoteEntity(entity, gameTime);
            }
        }

        private void ProcessLocalPlayer(Entity entity, GameTime gameTime)
        {
            // Local players can be moved automatically, then report their status if needed
            var playerTransform = entity.GetComponent<TransformComponent>();

            var playerSkin = entity.GetComponent<SkinComponent>();
            var playerDescriptor = entity.GetComponent<SpriteComponent>().SpriteDescriptor;
            
            // Move the camera
            ServiceManager.Camera.Move(-lastTransformVector);


            // Apply the multiplier to the velocity and move the position
            Vector2 nextPosition = playerTransform.LocalPosition;

            // Clamp the x and y so the player won't keep walking offscreen
            //float nextX = MathHelper.Clamp(nextPosition.X + playerDescriptor.BoundingBox.X, 0, ServiceManager.Map.WorldSizePixels.X / 2 - playerDescriptor.BoundingBox.Width);
            //float nextY = MathHelper.Clamp(nextPosition.Y + playerDescriptor.BoundingBox.Y, 0, ServiceManager.Map.WorldSizePixels.Y / 2 - playerDescriptor.BoundingBox.Height);

        
            //playerTransform.LocalPosition = new Vector2(nextX - playerDescriptor.BoundingBox.X, nextY - playerDescriptor.BoundingBox.Y);
            playerTransform.LastLocalPosition = playerTransform.LocalPosition;
            playerTransform.LocalPosition += playerTransform.Velocity;

            if (playerTransform.Velocity.X != playerTransform.Velocity.Y)
            {
                if (playerTransform.Velocity.X < 0)
                    playerTransform.DirectionalCache = DirectionalCache.Left;
                
                if (playerTransform.Velocity.X > 0)
                    playerTransform.DirectionalCache = DirectionalCache.Right;
                
                if (playerTransform.Velocity.Y > 0)
                    playerTransform.DirectionalCache = DirectionalCache.Down;
                
                if (playerTransform.Velocity.Y < 0)
                    playerTransform.DirectionalCache = DirectionalCache.Up;
            }

            var directionalChange = (playerTransform.Velocity != playerTransform.LastVelocity &&
                                     playerTransform.Velocity != Vector2.Zero);


            if ((_lastReaction > MovementRate && playerTransform.Velocity != Vector2.Zero) ||  directionalChange)
            {
                // Alert the server out this change in events if needed
                var packet = new NotifyMovementPacket(playerTransform.Velocity, playerTransform.LocalPosition, _idToMonitor);
                NetworkManager.Instance.SendPacket(packet);

                // Reset reaction timer
                _lastReaction = 0f;
            }

            // Increment reaction timer
            _lastReaction += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void ProcessRemoteEntity(Entity entity, GameTime gameTime)
        {
            // Interpolate
            foreach (var entityInterpolator in _entityInterpolators)
                entityInterpolator.Value.PeformInterpolationStep(gameTime, MovementRate);
        }

        public override void HandleInput(InputState inputState)
        {

        }
    }
}
