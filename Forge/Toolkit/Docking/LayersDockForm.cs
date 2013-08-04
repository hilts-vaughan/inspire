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
using Toolkit.Docking.Content;

namespace Toolkit.Docking
{
    public partial class LayersDockForm : ToolWindow
    {
        private MapForm _mapContext;

        public LayersDockForm()
        {
            InitializeComponent();
            TabText = "Layers";
        }

        /// <summary>
        /// Binds a particular set of layers to this form
        /// </summary>
        /// <param name="gameMap"></param>
        public void BindLayers(MapForm  mapForm)
        {
            // Bind the context
            _mapContext = mapForm;

            listLayers.Items.Clear();            

            // Begin populating the view
            for (int index = mapForm.Map.Layers.Count - 1; index >= 0; index--)
            {
                var layer = mapForm.Map.Layers[index];
                var item = new ListViewItem(new string[] {"Default Layer"});
                item.Tag = index;



                listLayers.Items.Add(item);
            }

            listLayers.Items[(mapForm.Map.Layers.Count - 1) - _mapContext.CurrentLayer].Selected = true;

        }

        private void listLayers_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listLayers.SelectedItems.Count == 0)
                return;

            var id =(int)  listLayers.SelectedItems[0].Tag;
            _mapContext.CurrentLayer = id;
        }

  

    }
}
