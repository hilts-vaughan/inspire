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
            this.imagesTree = new System.Windows.Forms.ImageList(this.components);
            this.imagesState = new System.Windows.Forms.ImageList(this.components);
            this.treeContent = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // imagesTree
            // 
            this.imagesTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesTree.ImageStream")));
            this.imagesTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesTree.Images.SetKeyName(0, "folder.png");
            this.imagesTree.Images.SetKeyName(1, "folder-open.png");
            this.imagesTree.Images.SetKeyName(2, "map.png");
            this.imagesTree.Images.SetKeyName(3, "screwdriver.png");
            this.imagesTree.Images.SetKeyName(4, "ghost.png");
            this.imagesTree.Images.SetKeyName(5, "xfn.png");
            // 
            // imagesState
            // 
            this.imagesState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesState.ImageStream")));
            this.imagesState.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesState.Images.SetKeyName(0, "control.png");
            // 
            // treeContent
            // 
            this.treeContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeContent.ImageIndex = 0;
            this.treeContent.ImageList = this.imagesTree;
            this.treeContent.Location = new System.Drawing.Point(0, 0);
            this.treeContent.Name = "treeContent";
            this.treeContent.PathSeparator = "/";
            this.treeContent.SelectedImageIndex = 0;
            this.treeContent.ShowPlusMinus = false;
            this.treeContent.ShowRootLines = false;
            this.treeContent.Size = new System.Drawing.Size(284, 261);
            this.treeContent.StateImageList = this.imagesState;
            this.treeContent.TabIndex = 0;
            this.treeContent.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeContent_NodeMouseDoubleClick);
            // 
            // ContentDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.treeContent);
            this.Name = "ContentDockForm";
            this.Text = "ContentDockForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeContent;
        private System.Windows.Forms.ImageList imagesTree;
        private System.Windows.Forms.ImageList imagesState;
    }
}