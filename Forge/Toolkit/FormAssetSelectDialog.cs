using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class FormAssetSelectDialog : Form
    {
        /// <summary>
        /// A dialog to select an asset from
        /// </summary>
        /// <param name="assetTypePath">A blank asset type is 'all'</param>
        public FormAssetSelectDialog()
        {
            InitializeComponent();

            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content\\");
        }

        public FormAssetSelectDialog(string defaultPath)
        {
            InitializeComponent();

            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content\\");

            foreach (TreeNode node in fileSystemTreeView1.Nodes[0].Nodes)
            {

                if (node.FullPath == defaultPath)
                {
                    fileSystemTreeView1.SelectedNode = node;
                    fileSystemTreeView1.SelectedNode.Expand();
                }
            }
        }

        

        private void fileSystemTreeView1_DoubleClick(object sender, EventArgs e)
        {

        }

        public string AssetPath { get; set; }

        private void fileSystemTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Do checks to make sure they're not directories
            if (IsFolder(ProjectSettings.Instance.Location + "\\" + e.Node.FullPath))
                return;

            //Set the AssetPath
            AssetPath = e.Node.FullPath;

            //Close the window
            Close();
        }

        private void fileSystemTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void fileSystemTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.FullPath.EndsWith(".png"))
            {
                using (FileStream fs = new FileStream((ProjectSettings.Instance.Location + "\\" + e.Node.FullPath), FileMode.Open))
                    previewBox.Image = Image.FromStream(fs);
            }
            else
            {
                previewBox.Image = null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AssetPath = "";
            Close();
        }

        /// <summary>
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="Path">File path</param>
        /// <returns>True if a folder</returns>
        public bool IsFolder(string path)
        {
            return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Do checks to make sure they're not directories
            if (IsFolder(ProjectSettings.Instance.Location + "\\" + fileSystemTreeView1.SelectedNode.FullPath))
                return;


            AssetPath = fileSystemTreeView1.SelectedNode.FullPath;
            Close();
        }

        private void previewBox_Click(object sender, EventArgs e)
        {

            if (previewBox.Image != null)
            {
                var form = new Form();
                form.Size = new Size(800, 600);
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                form.Controls.Add(pictureBox);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.BackColor = Color.Black;
                form.Text = "Asset Preview";
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.Size = new Size(previewBox.Image.Size.Width + 10, previewBox.Image.Size.Height + 40);
                pictureBox.Image = previewBox.Image;
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox.Click += (sender1, e1) => form.Close();
                form.ShowDialog();
            }
        }


    }
}
