using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class FormEventEditor : Form
    {
        public FormEventEditor()
        {
            InitializeComponent();
        }

        private void pictureSprite_Click(object sender, EventArgs e)
        {
            string asset = SelectAsset();

            //Make sure the asset is valud
            if(string.IsNullOrEmpty(asset))
                return;

            Bitmap bmp = (Bitmap)Image.FromFile(ProjectSettings.Instance.Location + "\\" + asset);
            pictureSprite.Image = bmp.Clone(new Rectangle(0, 128, 48, 64), PixelFormat.DontCare);


                //TODO: Edit underlying event path


            }


        /// <summary>
        /// Retrieves a given asset from the disk
        /// </summary>
        /// <returns></returns>
        string SelectAsset()
        {
            FormAssetSelectDialog assetSelectDialog = new FormAssetSelectDialog();
            assetSelectDialog.ShowDialog();
            return assetSelectDialog.AssetPath;
        }


    }
}
