using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Toolkit.Mapping
{
    public static class MapEditorGlobals
    {


        /// <summary>
        /// The currently selected tiles on the sprite sheet
        /// </summary>
        public static Rectangle RectangleSelectedTiles { get; set; }

        public static Rectangle RectangleEntitySelector { get; set; }

        public static bool ShowGround { get; set; }

        /// <summary>
        /// The active texture given the map
        /// </summary>
        public static Texture2D CurrentActiveTexture { get; set; }

        /// <summary>
        /// Drawing tiles?
        /// </summary>
        public static bool Drawing = true;




    }
}
