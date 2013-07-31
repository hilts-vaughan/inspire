﻿namespace Toolkit
{
    partial class FormAssetSelectDialog
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
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.fileSystemTreeView1 = new C2C.FileSystem.FileSystemTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.previewBox.Location = new System.Drawing.Point(295, 0);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(214, 269);
            this.previewBox.TabIndex = 3;
            this.previewBox.TabStop = false;
            this.previewBox.Click += new System.EventHandler(this.previewBox_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 273);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(352, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // fileSystemTreeView1
            // 
            this.fileSystemTreeView1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fileSystemTreeView1.ImageIndex = 0;
            this.fileSystemTreeView1.Location = new System.Drawing.Point(0, 0);
            this.fileSystemTreeView1.Name = "fileSystemTreeView1";
            this.fileSystemTreeView1.SelectedImageIndex = 0;
            this.fileSystemTreeView1.ShowFiles = true;
            this.fileSystemTreeView1.Size = new System.Drawing.Size(289, 298);
            this.fileSystemTreeView1.TabIndex = 2;
            this.fileSystemTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileSystemTreeView1_AfterSelect);
            this.fileSystemTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileSystemTreeView1_NodeMouseClick);
            this.fileSystemTreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileSystemTreeView1_NodeMouseDoubleClick);
            this.fileSystemTreeView1.DoubleClick += new System.EventHandler(this.fileSystemTreeView1_DoubleClick);
            // 
            // FormAssetSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 298);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.fileSystemTreeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAssetSelectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asset Selector";
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C2C.FileSystem.FileSystemTreeView fileSystemTreeView1;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

    }
}