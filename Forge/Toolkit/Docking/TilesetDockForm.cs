using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toolkit.Mapping;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace Toolkit.Docking
{
    public partial class TilesetDockForm : ToolWindow
    {
        public TilesetDockForm()
        {
            InitializeComponent();
            TabText = "Tilesets";
        
        Application.Idle += ApplicationOnIdle;

        }

        private DateTime then = DateTime.Now;

        private void ApplicationOnIdle(object sender, EventArgs eventArgs)
        {
            if ((DateTime.Now - then).Milliseconds > 500)
            {
                then = DateTime.Now;
                tilesetRenderControl1.Invalidate();
            }
        }

        private void tilesetRenderControl1_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse.WindowHandle = tilesetRenderControl1.Handle;
            var mouseState = Mouse.GetState();

            var startX = mouseState.X/32;
            var startY = mouseState.Y/32;
            startX *= 32;
            startY *= 32;


            var prevX = -1;
            var prevY = -1;

            while (mouseState.LeftButton == ButtonState.Pressed)
            {
                mouseState = Mouse.GetState();

  


                var x = mouseState.X/32;
                var y = mouseState.Y/32;
                x *= 32;
                y *= 32;

                if (prevX == x && prevY == y)
                    continue;

                prevX = x;
                prevY = y;


                MapEditorGlobals.RectangleSelectedTiles = new Microsoft.Xna.Framework.Rectangle(startX, startY, 32 + (x - startX) , 32 + (y - startY));



                // Compute the box like so


                Refresh();
                Application.DoEvents();
            }

            mouseState = Mouse.GetState();




        }
    }
}
