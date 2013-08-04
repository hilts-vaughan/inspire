namespace Toolkit.Docking
{
    partial class TilesetDockForm
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
            this.tilesetRenderControl1 = new Toolkit.Controls.Rendering.TilesetRenderControl();
            this.SuspendLayout();
            // 
            // tilesetRenderControl1
            // 
            this.tilesetRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetRenderControl1.Location = new System.Drawing.Point(0, 0);
            this.tilesetRenderControl1.Name = "tilesetRenderControl1";
            this.tilesetRenderControl1.Size = new System.Drawing.Size(284, 261);
            this.tilesetRenderControl1.SpriteBatch = null;
            this.tilesetRenderControl1.TabIndex = 0;
            this.tilesetRenderControl1.Text = "tilesetRenderControl1";
            this.tilesetRenderControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tilesetRenderControl1_MouseDown);
            // 
            // TilesetDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tilesetRenderControl1);
            this.Name = "TilesetDockForm";
            this.TabText = "Tileset";
            this.Text = "TilesetDockForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Rendering.TilesetRenderControl tilesetRenderControl1;
    }
}