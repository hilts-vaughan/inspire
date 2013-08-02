﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlastersGame.Network;
using Inspire.Network.Packets.Client;
using Inspire.Shared.Crypto;
using Toolkit.Mapping;

namespace Toolkit
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Disable the login button
            buttonLogin.Enabled = false;

            // Send a packet requesting a login
            var request = new EditorLoginRequestPacket(textUsername.Text, HashHelper.CalculateSha512Hash(textPassword.Text));
            NetworkManager.Instance.SendPacket(request);

        }


  
    }
}
