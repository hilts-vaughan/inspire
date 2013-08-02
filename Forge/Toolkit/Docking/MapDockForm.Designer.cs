namespace Toolkit.Docking
{
    partial class MapDockForm
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
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.mapRenderControl1 = new Toolkit.Controls.Rendering.MapRenderControl();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(267, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 261);
            this.vScrollBar1.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 244);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(267, 17);
            this.hScrollBar1.TabIndex = 1;
            // 
            // mapRenderControl1
            // 
            this.mapRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapRenderControl1.Location = new System.Drawing.Point(0, 0);
            this.mapRenderControl1.Name = "mapRenderControl1";
            this.mapRenderControl1.Size = new System.Drawing.Size(267, 244);
            this.mapRenderControl1.SpriteBatch = null;
            this.mapRenderControl1.TabIndex = 2;
            this.mapRenderControl1.Text = "mapRenderControl1";
            // 
            // MapDockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.mapRenderControl1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Name = "MapDockForm";
            this.Text = "MapDockForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private Controls.Rendering.MapRenderControl mapRenderControl1;
    }
}