using System.Collections.Generic;
using Inspire.Shared.Components;
using Inspire.Shared.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Inspire.GameEngine.ScreenManager;

namespace Inspire.GameEngine.Services
{
    /// <summary>
    /// A service container manages a bunch of game related services and entity management.   
    /// </summary>
    public class ServiceContainer
    {


        public ServiceContainer( ContentManager contentManager, GraphicsDevice device)
        {
            _contentManager = contentManager;
            GraphicsDevice = device;
            EntityCollection = new EntityCollection();
        }

        public Entity RetrieveEntityByID(ulong userID)
        {
            foreach (var entity in EntityCollection.Entities)
            {
                if (entity.ID == userID)
                    return entity;
            }

            return null;
        }


        public GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// Grab the camera
        /// </summary>
        public Camera2D Camera { get; set; }

        private List<Service> _services = new List<Service>();
        private ContentManager _contentManager;

        public void AddService(Service service)
        {
            service.ServiceManager = this;
            service.ContentManager = _contentManager;
            service.Initialize();
            _services.Add(service);
        }

        public delegate void EntityEvent(Entity entity);

        public event EntityEvent EntityAdded;
        public event EntityEvent EntityRemoved;

        protected virtual void OnEntityRemoved(Entity entity)
        {
            EntityEvent handler = EntityRemoved;
            if (handler != null) handler(entity);
        }

        protected virtual void OnEntityAdded(Entity entity)
        {
            EntityEvent handler = EntityAdded;
            if (handler != null) handler(entity);
        }


        private List<Entity> _toRemove = new List<Entity>();

        /// <summary>
        /// Removes an entity from the server con   tainer, also fires off an event to notify.
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity)
        {
            _toRemove.Add(entity);
        }

        public void RemoveEntityByID(ulong entityID)
        {
            foreach (var entity in EntityCollection.Entities)
            {
                if (entity.ID == entityID)
                {
                    _toRemove.Add(entity);
                    return;
                }
            }

            throw new KeyNotFoundException("The given entity was not found on the client. Double request sent? ");
        }

        /// <summary>
        /// Adds an entity to the server container, also fires off events for notifications.
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(Entity entity)
        {
            EntityCollection.Entities.Add(entity);
            OnEntityAdded(entity);
        }

        /// <summary>
        /// A list of entities contained in this service system.
        /// </summary>
        public EntityCollection EntityCollection { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw everything
            foreach (var service in _services)
                service.Draw(spriteBatch);

        }

        public void UpdateService(GameTime gameTime)
        {

            foreach (var service in _services)
                service.Update(gameTime);

            foreach (var toRemove in _toRemove)
            {
                EntityCollection.Entities.Remove(toRemove);
                OnEntityRemoved(toRemove);
            }
            _toRemove.Clear();


        }

        public void UpdateInput(InputState inputState)
        {

          

            foreach (var service in _services)
                service.HandleInput(inputState);
        }


    }
}
