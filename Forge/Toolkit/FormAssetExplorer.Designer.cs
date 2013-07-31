namespace Toolkit
{
    partial class FormAssetExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAssetExplorer));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Audio", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Graphics");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Misc");
            this.contextAssets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAssetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeAssets = new System.Windows.Forms.TreeView();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.fileSystemTreeView1 = new C2C.FileSystem.FileSystemTreeView();
            this.contextAssets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextAssets
            // 
            this.contextAssets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAssetToolStripMenuItem,
            this.removeAssetToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.previewToolStripMenuItem});
            this.contextAssets.Name = "contextAssets";
            this.contextAssets.Size = new System.Drawing.Size(158, 126);
            // 
            // addAssetToolStripMenuItem
            // 
            this.addAssetToolStripMenuItem.Image = global::Toolkit.Properties.Resources.image_add;
            this.addAssetToolStripMenuItem.Name = "addAssetToolStripMenuItem";
            this.addAssetToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addAssetToolStripMenuItem.Text = "Add Asset...";
            this.addAssetToolStripMenuItem.Click += new System.EventHandler(this.addAssetToolStripMenuItem_Click);
            // 
            // removeAssetToolStripMenuItem
            // 
            this.removeAssetToolStripMenuItem.Image = global::Toolkit.Properties.Resources.image_delete;
            this.removeAssetToolStripMenuItem.Name = "removeAssetToolStripMenuItem";
            this.removeAssetToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.removeAssetToolStripMenuItem.Text = "Remove Asset...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::Toolkit.Properties.Resources.folder_add;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem2.Text = "New Group...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::Toolkit.Properties.Resources.folder_delete;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem1.Text = "Remove Group";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Image = global::Toolkit.Properties.Resources.image_link;
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "music.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            this.imageList1.Images.SetKeyName(2, "image.png");
            this.imageList1.Images.SetKeyName(3, "rainbow.png");
            // 
            // treeAssets
            // 
            this.treeAssets.ContextMenuStrip = this.contextAssets;
            this.treeAssets.ImageIndex = 0;
            this.treeAssets.ImageList = this.imageList1;
            this.treeAssets.Location = new System.Drawing.Point(0, 165);
            this.treeAssets.Name = "treeAssets";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Node0";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Node1";
            treeNode3.ImageIndex = 0;
            treeNode3.Name = "nodeAudio";
            treeNode3.SelectedImageIndex = 0;
            treeNode3.Text = "Audio";
            treeNode4.ImageKey = "image.png";
            treeNode4.Name = "nodeGraphics";
            treeNode4.SelectedImageKey = "image.png";
            treeNode4.Text = "Graphics";
            treeNode5.ImageKey = "rainbow.png";
            treeNode5.Name = "nodeMisc";
            treeNode5.SelectedImageKey = "rainbow.png";
            treeNode5.Text = "Misc";
            this.treeAssets.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            this.treeAssets.SelectedImageIndex = 0;
            this.treeAssets.Size = new System.Drawing.Size(292, 101);
            this.treeAssets.TabIndex = 0;
            this.treeAssets.Visible = false;
            this.treeAssets.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeAssets_AfterLabelEdit);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.IncludeSubdirectories = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            // 
            // fileSystemTreeView1
            // 
            this.fileSystemTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSystemTreeView1.ImageIndex = 0;
            this.fileSystemTreeView1.Location = new System.Drawing.Point(0, 0);
            this.fileSystemTreeView1.Name = "fileSystemTreeView1";
            this.fileSystemTreeView1.SelectedImageIndex = 0;
            this.fileSystemTreeView1.ShowFiles = true;
            this.fileSystemTreeView1.Size = new System.Drawing.Size(280, 266);
            this.fileSystemTreeView1.TabIndex = 1;
            this.fileSystemTreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileSystemTreeView1_NodeMouseDoubleClick);
            // 
            // FormAssetExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 266);
            this.Controls.Add(this.fileSystemTreeView1);
            this.Controls.Add(this.treeAssets);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAssetExplorer";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
            this.ShowIcon = false;
            this.TabText = "Asset Explorer";
            this.Text = "Asset Explorer";
            this.Shown += new System.EventHandler(this.FormAssetExplorer_Shown);
            this.contextAssets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeAssets;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextAssets;
        private System.Windows.Forms.ToolStripMenuItem addAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAssetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private C2C.FileSystem.FileSystemTreeView fileSystemTreeView1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;

    }
}