namespace Toolkit.Controls
{
    partial class MapListView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("East Zanarkand");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("West Zanarkand");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("North Zanarkand");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Fountain of the Dead");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Zanarkand", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Entrance");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Soul Pool");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Soul Burial Grounds", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Dream Forest", 1, 1);
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Staircase of the Damned");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Xero (Floor 1)");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Xero (Floor 2)");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Heavens\' Mirror", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapListView));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageListMapView = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageKey = "photo.png";
            this.treeView1.ImageList = this.imageListMapView;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node1";
            treeNode1.Text = "East Zanarkand";
            treeNode2.Name = "Node2";
            treeNode2.Text = "West Zanarkand";
            treeNode3.Name = "Node3";
            treeNode3.Text = "North Zanarkand";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Fountain of the Dead";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "Node0";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "Zanarkand";
            treeNode6.Name = "Node1";
            treeNode6.Text = "Entrance";
            treeNode7.Name = "Node3";
            treeNode7.Text = "Soul Pool";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "Node0";
            treeNode8.SelectedImageIndex = 1;
            treeNode8.Text = "Soul Burial Grounds";
            treeNode9.ImageIndex = 1;
            treeNode9.Name = "Node5";
            treeNode9.SelectedImageIndex = 1;
            treeNode9.Text = "Dream Forest";
            treeNode10.Name = "Node7";
            treeNode10.Text = "Staircase of the Damned";
            treeNode11.Name = "Node9";
            treeNode11.Text = "Xero (Floor 1)";
            treeNode12.Name = "Node10";
            treeNode12.Text = "Xero (Floor 2)";
            treeNode13.ImageIndex = 1;
            treeNode13.Name = "Node6";
            treeNode13.SelectedImageIndex = 1;
            treeNode13.Text = "Heavens\' Mirror";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode8,
            treeNode9,
            treeNode13});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(150, 150);
            this.treeView1.TabIndex = 17;
            // 
            // imageListMapView
            // 
            this.imageListMapView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMapView.ImageStream")));
            this.imageListMapView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMapView.Images.SetKeyName(0, "photo.png");
            this.imageListMapView.Images.SetKeyName(1, "folder.png");
            // 
            // MapListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Name = "MapListView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageListMapView;
    }
}
