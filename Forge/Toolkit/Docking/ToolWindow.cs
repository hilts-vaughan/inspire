using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Toolkit.Docking
{
    public partial class ToolWindow : DockContent
    {

        public ToolWindow()
        {
            InitializeComponent();

            TabText = "History";

        }



    }
}
