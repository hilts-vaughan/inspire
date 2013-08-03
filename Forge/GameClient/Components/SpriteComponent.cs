using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspire.Shared.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Inspire.GameEngine.ScreenManager.Components
{
    public class SpriteComponent : Component
    {
        /// <summary>
        /// The texture assosciated with this particular component
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The frame of animation that is assoicated with this component
        /// </summary>
        public byte AnimationFrame { get; set; }

        /// <summary>
        /// The descriptor object definiing this sprite
        /// </summary>
        public SpriteDescriptor SpriteDescriptor { get; set; }

        public float LastFrameTime { get; set; }
    }

    public class SpriteDescriptor
    {
    }
}
