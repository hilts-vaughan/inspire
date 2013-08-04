namespace Toolkit.Docking
{
    partial class LayersDockForm
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
            this.listLayers = new Toolkit.Controls.MyListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listLayers
            // 
            this.listLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLayers.Location = new System.Drawing.Point(0, 0);
            this.listLayers.Name = "listLayers";
            this.listLayers.Size = new System.Drawing.Size(284, 261);
            this.listLayers.TabIndex = 0;
            this.listLayers.UseCompatibleStateImageBehavior = false;
            this.listLayers.View = System.Windows.Forms.View.Details;
            this.listLayers.SelectedIndexChanged += new System.EventHandler(this.listLayers_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Layer Name";
            this.columnHeader1.Width = 169;
            // 
            // LayersDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.listLayers);
            this.Name = "LayersDockForm";
            this.Text = "LayersDockForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MyListView listLayers;
        private System.Windows.Forms.ColumnHeader columnHeader1;



    }
}