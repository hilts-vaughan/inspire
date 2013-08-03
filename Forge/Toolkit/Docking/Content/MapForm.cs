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

namespace Toolkit.Docking.Content
{
    public partial class MapForm : ToolWindow, ISaveable
    {
        public MapForm()
        {
            InitializeComponent();
        }

        private MapTemplate _template;

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

            mapView.SetMap(map);
        }

        public void Save()
        {
            MessageBox.Show("I'm saved");
        }

        private void timerRedraw_Tick(object sender, EventArgs e)
        {
            mapView.Invalidate();
        }
    }
}
