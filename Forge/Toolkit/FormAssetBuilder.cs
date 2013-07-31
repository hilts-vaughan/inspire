using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class FormAssetBuilder : Form
    {
        //Builds the content for this session
        private ContentBuilder _contentBuilder = new ContentBuilder();

        public FormAssetBuilder()
        {
            InitializeComponent();
        }

        private void FormAssetBuilder_Shown(object sender, EventArgs e)
        {
          
        }
    }
}
