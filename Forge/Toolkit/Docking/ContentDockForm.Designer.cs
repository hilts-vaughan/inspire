namespace Toolkit.Docking
{
    partial class ContentDockForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentDockForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.imagesTree = new System.Windows.Forms.ImageList(this.components);
            this.imagesState = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // imagesTree
            // 
            this.imagesTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesTree.ImageStream")));
            this.imagesTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesTree.Images.SetKeyName(0, "blue-folder-horizontal.png");
            this.imagesTree.Images.SetKeyName(1, "blue-folder-horizontal-open.png");
            // 
            // imagesState
            // 
            this.imagesState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesState.ImageStream")));
            this.imagesState.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesState.Images.SetKeyName(0, "control.png");
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imagesTree;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Node2";
            treeNode2.Name = "Node3";
            treeNode2.Text = "Node3";
            treeNode3.Name = "Node1";
            treeNode3.StateImageKey = "control.png";
            treeNode3.Text = "Node1";
            treeNode4.Name = "Node0";
            treeNode4.StateImageKey = "control.png";
            treeNode4.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(284, 261);
            this.treeView1.StateImageList = this.imagesState;
            this.treeView1.TabIndex = 0;
            // 
            // ContentDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.treeView1);
            this.Name = "ContentDockForm";
            this.Text = "ContentDockForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imagesTree;
        private System.Windows.Forms.ImageList imagesState;
    }
}