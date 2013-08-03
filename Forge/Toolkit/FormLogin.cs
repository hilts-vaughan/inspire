using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Crypto;
using Lidgren.Network;
using Toolkit.Configuration;
using Toolkit.Mapping;

namespace Toolkit
{
    public partial class FormLogin : Form
    {
        private bool _leaveOk = false;

        public FormLogin()
        {
            InitializeComponent();

            // Register for event
            PacketService.RegisterPacket<LoginResultPacket>(HandleLogin);

            // Get the configuration stuff
            AppConfiguration.Instance.Deserialize();

            // Setup binding
            checkBox1.DataBindings.Add("Checked", AppConfiguration.Instance, "RememberMe");

            if (AppConfiguration.Instance.RememberMe)
            {
                textUsername.Text = AppConfiguration.Instance.Username;
                textPassword.Text = AppConfiguration.Instance.Password;
            }


        }

        private void HandleLogin(LoginResultPacket loginResultPacket)
        {
            if (loginResultPacket.Result == LoginResultPacket.LoginResult.Succesful)
            {
                AppConfiguration.Instance.Username = textUsername.Text;
                AppConfiguration.Instance.Password = textPassword.Text;
                _leaveOk = true;
                Close();
            }
            else
                MessageBox.Show(
                    "Your login credentials were rejected. Ensure credentials are correct and your account is authorized to login.");

            buttonLogin.Enabled = true;
        }

        private bool _pendingLogin = false;

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Disable the login button
            buttonLogin.Enabled = false;
            _pendingLogin = true;

            // Send a packet requesting a login
            var request = new EditorLoginRequestPacket(textUsername.Text, HashHelper.CalculateSha512Hash(textPassword.Text));
            NetworkManager.Instance.SendPacket(request);

        }

        private void pollStatus_Tick(object sender, EventArgs e)
        {
            if (_pendingLogin)
                return;

            if (NetworkManager.Instance.Status == NetConnectionStatus.Connected)
            {
                labelStatus.ForeColor = Color.Green;
                labelStatus.Text = "Online";
                buttonLogin.Enabled = true;
            }
            else
            {
                labelStatus.ForeColor = Color.Red;
                labelStatus.Text = "Offline";
                buttonLogin.Enabled = false;
            }
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            var formSettings = new FormSettings();
            formSettings.ShowDialog();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_leaveOk)
                Application.Exit();
        }



    }
}
