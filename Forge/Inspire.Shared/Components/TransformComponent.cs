using Microsoft.Xna.Framework;

namespace Inspire.Shared.Components
{
    /// <summary>
    /// The transformation component contains informationa about world transformations for an entity.
    /// </summary>
    public class TransformComponent : Component
    {
        private Vector2 _lastVelocity;
        private Vector2 _velocity;

        public TransformComponent(Vector2 localPosition, Vector2 size)
        {
            LocalPosition = localPosition;
            ServerPosition = localPosition;
            LastLocalPosition = localPosition;
            Size = size;

            DirectionalCache = DirectionalCache.Down;            
        }

        public TransformComponent()
        {
            
        }

        /// <summary>
        /// The current velocity of this component
        /// </summary>
        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _lastVelocity = _velocity;
                _velocity = value;
            }
        }

        /// <summary>
        /// The previous velocity, set automatically from when Velocity is set.
        /// </summary>
        public Vector2 LastVelocity
        {
            get { return _lastVelocity; }
        }

        /// <summary>
        /// The local position of this entity
        /// </summary>
        public Vector2 LocalPosition { get; set; }

        /// <summary>
        /// The server position of this entity
        /// </summary>
        public Vector2 ServerPosition { get; set; }

        /// <summary>
        /// The last local position of this entity
        /// </summary>
        public Vector2 LastLocalPosition { get; set; }

        /// <summary>
        /// The size this particular entity occupies
        /// </summary>
        public Vector2 Size { get; set; }

        public DirectionalCache DirectionalCache { get; set; }
    }

    public enum DirectionalCache
    {
        Up,
        Down,
        Left,
        Right
    }

}
