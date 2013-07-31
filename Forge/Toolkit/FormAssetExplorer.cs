using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class FormAssetExplorer : ToolWindow
    {
        public FormAssetExplorer()
        {
            InitializeComponent();

          
        }


        /// <summary>
        /// Loads the music resources into the node for this project
        /// </summary>
        private void LoadAssets()
        {
            //TODO: Use recursion to implement folder creation better

            //Prepare...
            foreach (var assetType in Enum.GetValues(typeof(AssetType)))
            {

                //Clear this assets
                treeAssets.Nodes[(int)assetType].Nodes.Clear();

                foreach (var directory in Directory.GetDirectories(ProjectSettings.Instance.Location + "\\Content\\" + assetType, "*.*", SearchOption.AllDirectories))
                {
                    //Create a directory info object on this directory
                    DirectoryInfo di = new DirectoryInfo(directory);

                    TreeNode node = new TreeNode(di.Name);
                    node.Name = di.Name;
                    node.StateImageKey = "folder.png";
                    node.ImageKey = "folder.png";
                    node.SelectedImageKey = "folder.png";

                    if (di.Parent.Name == assetType.ToString())
                    {
                        treeAssets.Nodes[(int)assetType].Nodes.Add(node);
                    }
                    else
                    {
                        treeAssets.Nodes[(int)assetType].Nodes[di.Parent.Name].Nodes.Add(node);
                    }

                }
            }




        }


        /// <summary>
        /// Adds an asset from the disk to the current project, and adds it to the project directory.
        /// </summary>
        /// <param name="assetName"></param>
        private void AddAssetFromDisk(string assetName)
        {

            Asset asset = new Asset();
            asset.Name = assetName;
            asset.DirectoryPath = treeAssets.SelectedNode.FullPath;

            TreeNode node = new TreeNode(assetName);
            node.Name = assetName;
            node.Tag = asset;
            node.StateImageKey = GetRootNode(treeAssets.SelectedNode).StateImageKey;
            node.ImageKey = GetRootNode(treeAssets.SelectedNode).ImageKey;
            node.SelectedImageKey = GetRootNode(treeAssets.SelectedNode).SelectedImageKey;

            treeAssets.SelectedNode.Nodes.Add(node);

        }

        private void FormAssetExplorer_Shown(object sender, EventArgs e)
        {
            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content");

            fileSystemWatcher1.Path = ProjectSettings.Instance.Location + "\\Content";
        }

        private void addAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();


            if (result == DialogResult.OK)
            {
                //If it has a tag, move up a node
                if (treeAssets.SelectedNode.StateImageKey != "folder.png" && treeAssets.SelectedNode.Parent != null)
                    treeAssets.SelectedNode = treeAssets.SelectedNode.Parent;

                string assetName = dialog.SafeFileName;

                if (treeAssets.SelectedNode.Nodes.ContainsKey(dialog.SafeFileName))
                {
                    //Search for an open key
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        if (treeAssets.SelectedNode.Nodes.ContainsKey("(" + i + ")" + dialog.SafeFileName))
                            continue;

                        assetName = "(" + i + ")" + assetName;
                        break;
                    }
                }

                //Copy the file over
                File.Copy(dialog.FileName, ProjectSettings.Instance.Location + "\\Content\\" + treeAssets.SelectedNode.FullPath + "\\"
                    + assetName, true);

                AddAssetFromDisk(assetName);

                //Save the project
                ProjectSettings.Instance.SaveProject();
            }

        }


        private TreeNode GetRootNode(TreeNode node)
        {
            while (node.Parent != null)
                node = node.Parent;
            return node;
        }

        private void treeAssets_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //This allows us to create the directory to match the virtual directory
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content");
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content");
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content");
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            //Load up the filesystem
            fileSystemTreeView1.Load(ProjectSettings.Instance.Location + "\\Content");
        }

        private void fileSystemTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Process.Start(ProjectSettings.Instance.Location + "\\" + e.Node.FullPath);
        }
    }

}
