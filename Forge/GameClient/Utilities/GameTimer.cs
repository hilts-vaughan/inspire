using System;
using Microsoft.Xna.Framework;

namespace Inspire.GameEngine.ScreenManager.Utilities
{
    /// <summary>
    /// A <see cref="GameTimer"/> provides callbacks for when a certain amount of time has elapased.
    /// This is useful for things like time limited games, delayed menu transitions and the like.
    /// </summary>
    public class GameTimer
    {

        /// <summary>
        /// Initializes a new <see cref="GameTimer"/>
        /// </summary>
        /// <param name="totalSeconds">The amount of time it will take for this timer to fire off.</param>
        public GameTimer(double totalSeconds)
        {
            TotalSeconds = totalSeconds;
        }


        /// <summary>
        /// This event fires off when the <see cref="GameTimer"/> has completed. 
        /// </summary>
        public EventHandler Completed;

        /// <summary>
        /// The amount of total seconds this timer takes to fire off.
        /// </summary>
        public double TotalSeconds { get; set; }

        /// <summary>
        /// The amount of progress [0 .. TotalSeconds] that the timer has made at the current polltime.
        /// </summary>
        public double TotalProgressSeconds { get; set; }


        /// <summary>
        /// Updates the timer with the given <see cref="GameTime"/> state.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            TotalProgressSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            if (TotalProgressSeconds >= TotalSeconds)
                Completed(null, null);
        }


    }

}
