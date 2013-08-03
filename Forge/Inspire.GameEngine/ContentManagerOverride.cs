using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Inspire.GameEngine
{
    public static class TextureLoader
    {
        public static Texture2D GetTexture(string path, GraphicsDevice device)
        {           
            path = "Content" + "\\" + path;
            var stream = new FileStream(path, FileMode.Open);
            var texture  = Texture2D.FromStream(device,stream);
            stream.Close();
            return texture;
        }



    }
}
