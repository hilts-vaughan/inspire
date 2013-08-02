namespace Toolkit
{
    partial class FormContentExplorer
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Maps");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Items");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Scripts");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormContentExplorer));
            this.treeContent = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeContent
            // 
            this.treeContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeContent.ImageIndex = 0;
            this.treeContent.ImageList = this.imageList1;
            this.treeContent.Location = new System.Drawing.Point(0, 0);
            this.treeContent.Name = "treeContent";
            treeNode1.ImageKey = "map.png";
            treeNode1.Name = "nodeMaps";
            treeNode1.SelectedImageKey = "map.png";
            treeNode1.Text = "Maps";
            treeNode2.ImageKey = "money.png";
            treeNode2.Name = "nodeItems";
            treeNode2.SelectedImageKey = "money.png";
            treeNode2.Text = "Items";
            treeNode3.ImageKey = "script.png";
            treeNode3.Name = "nodeScripts";
            treeNode3.SelectedImageKey = "script.png";
            treeNode3.Text = "Scripts";
            this.treeContent.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeContent.SelectedImageIndex = 0;
            this.treeContent.Size = new System.Drawing.Size(284, 262);
            this.treeContent.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "book_error.png");
            this.imageList1.Images.SetKeyName(1, "map.png");
            this.imageList1.Images.SetKeyName(2, "script.png");
            this.imageList1.Images.SetKeyName(3, "money.png");
            // 
            // FormContentExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.treeContent);
            //this.HideOnClose = true;
            this.Name = "FormContentExplorer";
            //this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            //this.TabText = "Content Explorer";
            this.Text = "Content Explorer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeContent;
        private System.Windows.Forms.ImageList imageList1;
    }
}