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
            this.scrollHorizontal = new System.Windows.Forms.HScrollBar();
            this.scrollVertical = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.Name = "mapView";
            this.mapView.Size = new System.Drawing.Size(659, 418);
            this.mapView.SpriteBatch = null;
            this.mapView.TabIndex = 2;
            this.mapView.Text = "mapRenderControl1";
            this.mapView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapView_MouseDown);
            // 
            // scrollHorizontal
            // 
            this.scrollHorizontal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollHorizontal.Location = new System.Drawing.Point(0, 418);
            this.scrollHorizontal.Name = "scrollHorizontal";
            this.scrollHorizontal.Size = new System.Drawing.Size(659, 17);
            this.scrollHorizontal.TabIndex = 1;
            // 
            // scrollVertical
            // 
            this.scrollVertical.Dock = System.Windows.Forms.DockStyle.Right;
            this.scrollVertical.Location = new System.Drawing.Point(659, 0);
            this.scrollVertical.Name = "scrollVertical";
            this.scrollVertical.Size = new System.Drawing.Size(17, 435);
            this.scrollVertical.TabIndex = 0;
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 435);
            this.Controls.Add(this.mapView);
            this.Controls.Add(this.scrollHorizontal);
            this.Controls.Add(this.scrollVertical);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.HideOnClose = false;
            this.Name = "MapForm";
            this.TabText = "Map";
            this.Text = "Map";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar scrollVertical;
        private System.Windows.Forms.HScrollBar scrollHorizontal;
        private Controls.Rendering.MapRenderControl mapView;

    }
}