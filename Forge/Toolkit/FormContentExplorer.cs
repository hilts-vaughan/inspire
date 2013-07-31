using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class FormContentExplorer : ToolWindow
    {
        public FormContentExplorer()
        {
            InitializeComponent();

            LoadMaps();
            LoadItems();
        }

        private void LoadMaps()
        {
            TreeNode node2 = new TreeNode("Zanarkand Ruins");
            node2.SelectedImageKey = "map.png";
            node2.StateImageKey = "map.png";
            node2.ImageKey = "map.png";
            treeContent.Nodes[0].Nodes.Add(node2);

            for (int i = 0; i < 50; i++)
            {
                TreeNode node = new TreeNode("Sample Map");
                node.SelectedImageKey = "map.png";
                node.StateImageKey = "map.png";
                node.ImageKey = "map.png";
                treeContent.Nodes[0].Nodes.Add(node);
            }
        
        
        }

        private void LoadItems()
        {
             for (int i = 0; i < 50; i++)
            {
                TreeNode node = new TreeNode("Sample Item");
                node.SelectedImageKey = "money.png";
                node.StateImageKey = "money.png";
                node.ImageKey = "money.png";
                treeContent.Nodes[1].Nodes.Add(node);
            }
        }
    }
}
