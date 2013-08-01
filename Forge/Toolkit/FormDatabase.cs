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
using Inspire.Network.Packets.Client.Content;
using Inspire.Network.Packets.Server;
using Inspire.Network.Packets.Server.Content;
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
            PacketService.RegisterPacket<ContentListResultPacket>(Handler);

        }






        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make the request
            var type = GetActiveContentPage().ContentType;
            var request = new ContentRequestPacket(type, lstIndex.SelectedIndex + 1);

            // Send the request
            NetworkManager.Instance.SendPacket(request);

        }

        private void Handler(ContentResultPacket contentResultPacket)
        {

            var page = GetActiveContentPage();

            // Clear all bindings first
            var contentGrid = tabContentPages.SelectedTab.Controls[0];
            BindingHelper.ClearBindings(contentGrid);

            page.BindTemplateObject(contentResultPacket.ContentObject);


        }

        private void Handler(ContentListResultPacket contentListResultPacket)
        {           
            lstIndex.Items.Clear();
            contentListResultPacket.EditorTemplateEntries.ForEach(x => lstIndex.Items.Add(x));
        }



        private IContentPage GetActiveContentPage()
        {
            var tabPage = tabContentPages.SelectedTab;
            var contentControl = tabPage.Controls[0];

            // Retrieve the value now
            var contentPage = (IContentPage) contentControl;
            return contentPage;
        }

        /// <summary>
        /// An event that fires when the tab page changes. This is used to make a list request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabContentPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make the request
            var type = GetActiveContentPage().ContentType;
            var request = new ContentListRequestPacket(type);

            // Send the request
            NetworkManager.Instance.SendPacket(request);

        }

        private void FormDatabase_Load(object sender, EventArgs e)
        {
            // Select the first item
            tabContentPages.SelectedIndex = 0;
            tabContentPages_SelectedIndexChanged(null, null);
        }


    }
}
