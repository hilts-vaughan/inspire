namespace Toolkit.Docking.Content
{
    partial class MapForm
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
            this.mapRenderControl1 = new Toolkit.Controls.Rendering.MapRenderControl();
            this.SuspendLayout();
            // 
            // mapRenderControl1
            // 
            this.mapRenderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapRenderControl1.Location = new System.Drawing.Point(0, 0);
            this.mapRenderControl1.Name = "mapRenderControl1";
            this.mapRenderControl1.Size = new System.Drawing.Size(284, 261);
            this.mapRenderControl1.SpriteBatch = null;
            this.mapRenderControl1.TabIndex = 0;
            this.mapRenderControl1.Text = "mapRenderControl1";
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.mapRenderControl1);
            this.HideOnClose = false;
            this.Name = "MapForm";
            this.TabText = "Map";
            this.Text = "Map";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Rendering.MapRenderControl mapRenderControl1;
    }
}