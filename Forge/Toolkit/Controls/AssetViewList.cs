using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toolkit.Controls
{
    public partial class AssetViewList : UserControl
    {
        public AssetViewList()
        {
            //Create our view here, dynamically
            string localPath = ProjectSettings.Instance.Location;

            InitializeComponent();
        }
    }
}
