using System;
using Inspire.Shared.Components;
using Microsoft.Xna.Framework;

namespace GameClient.Services.Movement
{
    /// <summary>
    /// This class is responsible for interpolating a given entity to their proper location on the client.
    /// </summary>
    public class EntityInterpolator
    {
        private readonly TransformComponent _transformComponent;
        private float _currentProgress;

        public EntityInterpolator(TransformComponent transformComponent)
        {
            _transformComponent = transformComponent;          
        }

        /// <summary>
        /// Resets the progress on this entity interpolator.
        /// </summary>
        public void ResetProgress(Vector2 newTarget)
        {
            _currentProgress = 0f;

            var oldGoal = _transformComponent.ServerPosition;
            _transformComponent.LastLocalPosition = oldGoal;
            _transformComponent.ServerPosition = newTarget;


        }


        /// <summary>
        /// Performs a given interpolation step with the given game time
        /// </summary>
        public void PeformInterpolationStep(GameTime gameTime, float rate)
        {

            _currentProgress += (float) gameTime.ElapsedGameTime.TotalSeconds * (1 / rate);
            _currentProgress = Math.Min(_currentProgress, 1);

            // Set the progress using linear interpolation
            _transformComponent.LocalPosition = Vector2.SmoothStep(_transformComponent.LastLocalPosition,
                                                             _transformComponent.ServerPosition, _currentProgress);
        }


    }
}
