using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        /// Drawing tiles?
        /// </summary>
        public static bool Drawing = true;



        public static string Username { get; set; }


        public static string Password { get; set; }

    }
}
