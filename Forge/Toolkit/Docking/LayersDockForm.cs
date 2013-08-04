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
        public void BindLayers(MapForm mapForm)
        {
            // Bind the context
            _mapContext = mapForm;

            listLayers.Items.Clear();

            // Begin populating the view
            for (int index = mapForm.Map.Layers.Count - 1; index >= 0; index--)
            {
                var layer = mapForm.Map.Layers[index];
                var item = new ListViewItem(new string[] { " " + layer.Name });
                item.Tag = index;

                if (layer.Visible)
                    item.ImageIndex = 0;



                listLayers.Items.Add(item);
            }

            listLayers.Items[LayerToIndex(mapForm)].Selected = true;

            _mapContext.Refresh();

        }

        private int LayerToIndex(MapForm mapForm)
        {
            return (mapForm.Map.Layers.Count - 1) - _mapContext.CurrentLayer;
        }



        private void listLayers_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {

        }

        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Layer Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            inputBox.ControlBox = false;
            inputBox.StartPosition = FormStartPosition.CenterScreen;


            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            var layer = _mapContext.Map.Layers[_mapContext.CurrentLayer].Name;
            ShowInputDialog(ref layer);
            _mapContext.Map.Layers[_mapContext.CurrentLayer].Name = layer;

            BindLayers(_mapContext);

        }

        private void listLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listLayers.SelectedItems.Count == 0)
                return;

            var id = (int)listLayers.SelectedItems[0].Tag;
            _mapContext.CurrentLayer = id;
        }


        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            var id = (int)listLayers.SelectedItems[0].Tag;

            if (id == _mapContext.Map.Layers.Count - 1)
                return;

            _mapContext.Map.Layers.Swap(id, id + 1);



            BindLayers(_mapContext);

            listLayers.SelectedIndices.Clear();
            listLayers.Items[(_mapContext.Map.Layers.Count - 1) - (id + 1)].Selected = true;
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            var id = (int)listLayers.SelectedItems[0].Tag;

            if (id == 0)
                return;

            _mapContext.Map.Layers.Swap(id, id - 1);



            BindLayers(_mapContext);

            listLayers.SelectedIndices.Clear();
            listLayers.Items[(_mapContext.Map.Layers.Count - 1) - (id - 1)].Selected = true;

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (_mapContext.Map.Layers.Count == 1)
            {
                MessageBox.Show(
                    "You can't have any less than a single layer. Consider adding another, and removing this one.");
                return;
            }

            var id = (int)listLayers.SelectedItems[0].Tag;

            _mapContext.Map.Layers.RemoveAt(id);
            _mapContext.CurrentLayer = 0;

            BindLayers(_mapContext);


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var id = (int)listLayers.SelectedItems[0].Tag;

            // Remove the layer
            _mapContext.Map.Layers.Insert(id, new MapLayer());

            BindLayers(_mapContext);
        }

        private void buttonToggleVisible_Click(object sender, EventArgs e)
        {

            var id = (int)listLayers.SelectedItems[0].Tag;
            _mapContext.Map.Layers[id].Visible = !_mapContext.Map.Layers[id].Visible; 

            BindLayers(_mapContext);
        }
    }
}
