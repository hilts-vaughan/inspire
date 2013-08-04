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


            while (mouseState.LeftButton == ButtonState.Pressed)
            {

                var x = mouseState.X/32;
                var y = mouseState.Y/32;
                x *= 32;
                y *= 32;




                MapEditorGlobals.RectangleSelectedTiles = new Microsoft.Xna.Framework.Rectangle(x, y, 32, 32);


                // Refresh and paginate
                Application.DoEvents();
                mouseState = Mouse.GetState();
            }


        }
    }
}
