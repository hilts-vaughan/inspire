﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolkit.Docking
{
    public partial class ContentDockForm : ToolWindow
    {
        public ContentDockForm()
        {
            InitializeComponent();
            TabText = "Content Explorer";
        }
    }
}