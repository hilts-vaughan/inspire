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
    public partial class MapDockForm : ToolWindow, ISaveable
    {
        public MapDockForm()
        {
            InitializeComponent();
            TabText = "New Map";
        }


        /// <summary>
        /// Allows this form to save itself
        /// </summary>
        public void Save()
        {
            
        }


    }
}