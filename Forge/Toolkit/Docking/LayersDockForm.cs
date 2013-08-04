using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.Shared.Models.Map;

namespace Toolkit.Docking
{
    public partial class LayersDockForm : ToolWindow
    {
        private GameMap _mapContext;

        public LayersDockForm()
        {
            InitializeComponent();
            TabText = "Layers";
        }

        /// <summary>
        /// Binds a particular set of layers to this form
        /// </summary>
        /// <param name="gameMap"></param>
        public void BindLayers(GameMap gameMap)
        {
            // Bind the context
            _mapContext = gameMap;

            listLayers.Items.Clear();            

            // Begin populating the view
            foreach (var layer in gameMap.Layers)
            {
                var item = new ListViewItem( new string[] { "Default Layer" } );
                item.Tag = layer;
                listLayers.Items.Add(item);
            }

        }

    }
}
