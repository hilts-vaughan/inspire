namespace Toolkit
{
    partial class FormDatabase
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
            this.tabContentPages = new System.Windows.Forms.TabControl();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstIndex = new System.Windows.Forms.ListBox();
            this.itemPage = new Toolkit.Controls.Database.ItemPage();
            this.tabContentPages.SuspendLayout();
            this.tabItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabContentPages
            // 
            this.tabContentPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabContentPages.Controls.Add(this.tabItems);
            this.tabContentPages.Controls.Add(this.tabPage2);
            this.tabContentPages.Location = new System.Drawing.Point(171, 0);
            this.tabContentPages.Name = "tabContentPages";
            this.tabContentPages.SelectedIndex = 0;
            this.tabContentPages.Size = new System.Drawing.Size(900, 575);
            this.tabContentPages.TabIndex = 1;
            this.tabContentPages.SelectedIndexChanged += new System.EventHandler(this.tabContentPages_SelectedIndexChanged);
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.itemPage);
            this.tabItems.Location = new System.Drawing.Point(4, 22);
            this.tabItems.Name = "tabItems";
            this.tabItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabItems.Size = new System.Drawing.Size(892, 549);
            this.tabItems.TabIndex = 0;
            this.tabItems.Text = "Items";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(892, 549);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstIndex
            // 
            this.lstIndex.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstIndex.FormattingEnabled = true;
            this.lstIndex.Items.AddRange(new object[] {
            "Item1"});
            this.lstIndex.Location = new System.Drawing.Point(0, 0);
            this.lstIndex.Name = "lstIndex";
            this.lstIndex.Size = new System.Drawing.Size(165, 575);
            this.lstIndex.TabIndex = 2;
            this.lstIndex.SelectedIndexChanged += new System.EventHandler(this.lstIndex_SelectedIndexChanged);
            // 
            // itemPage
            // 
            this.itemPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemPage.Enabled = false;
            this.itemPage.Location = new System.Drawing.Point(3, 3);
            this.itemPage.Name = "itemPage";
            this.itemPage.Size = new System.Drawing.Size(886, 543);
            this.itemPage.TabIndex = 1;
            // 
            // FormDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 575);
            this.Controls.Add(this.lstIndex);
            this.Controls.Add(this.tabContentPages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDatabase";
            this.ShowIcon = false;
            this.Text = "Database";
            this.Load += new System.EventHandler(this.FormDatabase_Load);
            this.tabContentPages.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabContentPages;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstIndex;
        public Controls.Database.ItemPage itemPage;

    }
}