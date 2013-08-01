using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlastersGame.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Models.Enums;
using Toolkit.Controls.Database;

namespace Toolkit
{
    public partial class FormDatabase : Form
    {
        public FormDatabase()
        {
            InitializeComponent();

            // Make a subtle request for an item, disable the form in the meanwhile
            PacketService.RegisterPacket<ContentResultPacket>(Handler);
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make the request
            var type = GetActiveContentPage().ContentType;
            var request = new ContentRequestPacket(type, lstIndex.SelectedIndex + 1);

            // Send the request
            NetworkManager.Instance.SendPacket(request);

            Enabled = false;
        }

        private void Handler(ContentResultPacket contentResultPacket)
        {

            var page = GetActiveContentPage();
            page.BindTemplateObject(contentResultPacket.ContentObject);

            Enabled = true; 
        }



        private IContentPage GetActiveContentPage()
        {
            var tabPage = tabContentPages.SelectedTab;
            var contentControl = tabPage.Controls[0];

            // Retrieve the value now
            var contentPage = (IContentPage) contentControl;
            return contentPage;
        }


    }
}
