using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Toolkit
{
    public partial class FormScript : DockContent 
    {
        public FormScript()
        {
            InitializeComponent();
            scintilla1.Margins[0].Width = 20;
            scintilla1.CharAdded += new EventHandler<ScintillaNet.CharAddedEventArgs>(scintilla1_CharAdded);
        }

        void scintilla1_CharAdded(object sender, ScintillaNet.CharAddedEventArgs e)
        {
            int pos = scintilla1.NativeInterface.GetCurrentPos();


            int length = pos - scintilla1.NativeInterface.WordStartPosition(pos, true);

            foreach (var entry in scintilla1.AutoComplete.List)
            {
                if( entry.StartsWith(scintilla1.GetWordFromPosition(pos)) || entry != string.Empty)
                        scintilla1.AutoComplete.Show(length);
                return;
            }
            

        
 

        }
    }
}
