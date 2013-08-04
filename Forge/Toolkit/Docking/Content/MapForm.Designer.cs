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
            this.mapView = new Toolkit.Controls.Rendering.MapRenderControl();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.Name = "mapView";
            this.mapView.Size = new System.Drawing.Size(284, 261);
            this.mapView.SpriteBatch = null;
            this.mapView.TabIndex = 0;
            this.mapView.Text = "mapRenderControl1";
            this.mapView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapView_MouseDown);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.mapView);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.HideOnClose = false;
            this.Name = "MapForm";
            this.TabText = "Map";
            this.Text = "Map";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Rendering.MapRenderControl mapView;
    }
}