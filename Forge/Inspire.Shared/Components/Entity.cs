using System;
using System.Collections.Generic;
using System.Linq;

namespace Inspire.Shared.Components
{
    /// <summary>
    /// An entity is a game object within the Blasters World.
    /// They have spatial cordinates, a size and are basic template for existing game objects.
    /// Anything that is interactive should derive from this. Players, bombs, crates etc.
    /// Even powerups are entities.
    /// </summary>
    public class Entity
    {

        public static ulong _counter;
        private ulong _id;
        public List<Component> Components { get; set; }

        /// <summary>
        /// The unique ID for this given entity.
        /// </summary>
        public ulong ID { get; set; }

        public Entity()
        {
            // Assign the entity a unique ID internally
            ID = _counter;
            _counter++;

            Components = new List<Component>();
        }

        /// <summary>
        /// Adds a component to this entity, attaching it for system consumption.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            Components.Add(component);
        }


        /// <summary>
        /// Removes a component of a given type from the entity.
        /// </summary>
        /// <param name="type"></param>
        public void RemoveComponent(Type type)
        {
            for (int index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                if (component.GetType() == type)
                {
                    Components.Remove(component);
                    return;
                }
            }

            //TODO: This logic could use some clean up
            // Perhaps, when the dictionary implementation falls in, it'll get better
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)Components.FirstOrDefault(c => c.GetType() == typeof(T));
        }

        /// <summary>
        /// Returns whether or not a particular entity has a component or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasComponent<T>() where T : Component
        {
            return GetComponent<T>() != null;
        }



    }
}
