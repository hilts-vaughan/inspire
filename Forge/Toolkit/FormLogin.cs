using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Toolkit.Mapping;

namespace Toolkit
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();

            MapEditorGlobals.Username = textBox1.Text;
            MapEditorGlobals.Password = textBox2.Text;

        }
    }
}
