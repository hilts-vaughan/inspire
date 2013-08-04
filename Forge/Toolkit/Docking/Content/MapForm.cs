using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Map;
using Inspire.Shared.Models.Templates;
using Microsoft.Xna.Framework.Input;
using Toolkit.Mapping;
using ButtonState = System.Windows.Forms.ButtonState;

namespace Toolkit.Docking.Content
{
    public partial class MapForm : ToolWindow, ISaveable
    {
        public MapForm()
        {
            InitializeComponent();

            Application.Idle += Application_Idle;

        }

        private DateTime then = DateTime.Now;

        void Application_Idle(object sender, EventArgs e)
        {
            if ((DateTime.Now - then).Milliseconds > 500)
            {
                then = DateTime.Now;
                Update();
            }
        }

        private MapTemplate _template;

        public GameMap Map { get; set; }

        public void SetBinding(object contentObject)
        {

            var genericTemplate = contentObject as IContentTemplate;
            _template = contentObject as MapTemplate;


            TabText = "[" + "Map" + "] " + genericTemplate.Name;
            Text = "[" + "Map" + "] " + genericTemplate.Name;
            Update();
            Invalidate();



            // Get our map
            var map = GameMap.FromTemplate(_template);
            Map = map;

            mapView.SetMap(map);
        }

        public void TryToMakeContext()
        {
            mapView.TryToMakeContext();
        }

        public void Save()
        {
            MessageBox.Show("I'm saved");
        }

        private void timerRedraw_Tick(object sender, EventArgs e)
        {
           
        }

        private void mapView_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse.WindowHandle = mapView.Handle;
            var mouseState = Mouse.GetState();

            var prevX = -1;
            var prevY = -1;

            while (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {

                mouseState = Mouse.GetState();

                var x = mouseState.X / 32;
                var y = mouseState.Y / 32;
    
                if(prevX == x && prevY == y)
                    continue;

                prevX = x;
                prevY = y;


                // Get vertical portion
                var curTexture = MapEditorGlobals.CurrentActiveTexture;
                var global = MapEditorGlobals.RectangleSelectedTiles;
                var gX = MapEditorGlobals.RectangleSelectedTiles.X/32;
                var gY = MapEditorGlobals.RectangleSelectedTiles.Y/32;

                var tY = (gY)*curTexture.Width/32;
                var tX = gX;
                var tileID = tY + tX;

                Map.Layers[0].MapTiles[x][y].TileId = tileID;
                

                // Refresh and paginate

                Refresh();
                Application.DoEvents();

            }
        }
    }
}
