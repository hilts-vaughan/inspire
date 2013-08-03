using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Client.Content;
using Inspire.Network.Packets.Server;
using Inspire.Network.Packets.Server.Content;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;
using Toolkit.Controls.Database;

namespace Toolkit
{
    public partial class FormDatabase : Form
    {
        private int openedID = -1;

        public FormDatabase()
        {
            InitializeComponent();

            // Make a subtle request for an item, disable the form in the meanwhile
            PacketService.RegisterPacket<ContentResultPacket>(Handler);
            PacketService.RegisterPacket<ContentListResultPacket>(Handler);
            PacketService.RegisterPacket<ContentSaveResultPacket>(Handler);

        }

        private void Handler(ContentSaveResultPacket contentSaveResultPacket)
        {
            // Do something based on the result
            if (contentSaveResultPacket.RequestResult == RequestResult.Failed)
                ShowMessageBox("An unexpected error occured while saving. Please try again. (The server rejected the request.)", "Server Response");

            // Fetch a new, clean list
            RequestContentList();

            Enabled = true;
        }


        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Send a release request quickly

            ReleaseContent();

            // Make the request
            var type = GetActiveContentPage().ContentType;
            var request = new ContentRequestPacket(type, (lstIndex.SelectedItem as EditorTemplateEntry).ID);


            // Send the request
            NetworkManager.Instance.SendPacket(request);


        }

        private void ReleaseContent()
        {
            var contentPage = GetActiveContentPage();

            if (contentPage.BoundObject != null)
            {
                if (openedID != contentPage.BoundObject.Id)
                    return;

                var releaseRequest = new ContentReleasePacket(contentPage.ContentType, contentPage.BoundObject.Id);
                NetworkManager.Instance.SendPacket(releaseRequest);
            }
        }

        private void Handler(ContentResultPacket contentResultPacket)
        {

            if (contentResultPacket.Locked)
            {
                ShowMessageBox("The server rejected your request for this content. This usually happens because someone else has it checked out.", "Server Response");
                return;
            }

            var page = GetActiveContentPage();

            // Clear all bindings first
            var contentGrid = tabContentPages.SelectedTab.Controls[0];
            BindingHelper.ClearBindings(contentGrid);

            page.BoundObject = contentResultPacket.ContentObject as IContentTemplate;
            page.BindTemplateObject(contentResultPacket.ContentObject);

            openedID = (contentResultPacket.ContentObject as IContentTemplate).Id;



        }

        public DialogResult ShowMessageBox(String message, String caption)
        {
            if (this.InvokeRequired)
            {
                return (DialogResult)this.Invoke(new PassStringStringReturnDialogResultDelegate(ShowMessageBox), message, caption);
            }
            return MessageBox.Show(this, message, caption);
        }

        public delegate DialogResult PassStringStringReturnDialogResultDelegate(String s1, String s2);

        private void Handler(ContentListResultPacket contentListResultPacket)
        {
            lstIndex.Items.Clear();
            lstIndex.BeginUpdate();
            contentListResultPacket.EditorTemplateEntries.ForEach(x => lstIndex.Items.Add(x));
            lstIndex.EndUpdate();
        }



        private IContentPage GetActiveContentPage()
        {
            var tabPage = tabContentPages.SelectedTab;
            var contentControl = tabPage.Controls[0];

            // Retrieve the value now
            var contentPage = (IContentPage)contentControl;
            return contentPage;
        }

        private void RequestContentList()
        {
            // Make the request
            var type = GetActiveContentPage().ContentType;
            var request = new ContentListRequestPacket(type);

            // Send the request
            NetworkManager.Instance.SendPacket(request);
        }

        /// <summary>
        /// An event that fires when the tab page changes. This is used to make a list request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabContentPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestContentList();
        }

        private void FormDatabase_Load(object sender, EventArgs e)
        {
            // Select the first item
            tabContentPages.SelectedIndex = 0;
            RequestContentList();
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            // Make the request
            var contentPage = GetActiveContentPage();
            var request = new ContentSaveRequestPacket(contentPage.BoundObject, contentPage.ContentType);

            // Send the request
            NetworkManager.Instance.SendPacket(request);

            // We need to ensure no double clicks are permitted / navigation away
            Enabled = false;
        }

        private void FormDatabase_Leave(object sender, EventArgs e)
        {

        }

        private void FormDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseContent();
        }

        private void lstIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }


    }
}
