using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlastersGame.Network;
using Inspire.Network.Packets.Client.Content;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;
using Toolkit.Controls.Database;

namespace Toolkit.Docking.Content
{
    public partial class GenericContentBindForm : ToolWindow, ISaveable
    {
        public GenericContentBindForm()
        {
            InitializeComponent();

            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }

        }

        private int _id;
        private ContentType _contentType;
        private object _bound;

        public void SetBinding(object contentObject, ContentType contentType)
        {
            
            var genericTemplate = contentObject as IContentTemplate;
            
            _id = genericTemplate.Id;
            _contentType = contentType;
            _bound = contentObject;

            // Determine what to do
            switch (contentType)
            {
                case ContentType.Item:
                    var itemTemplate = contentObject as ItemTemplate;
                    var itemPage = new ItemPage();
                    itemPage.Dock = DockStyle.Fill;
                    Controls.Add(itemPage);
                    itemPage.BoundObject = itemTemplate;
                    itemPage.BindTemplateObject(contentObject);
                    break;
                case ContentType.Skill:
                    var skillTemplate = contentObject as SkillTemplate;
                    var skillPage = new SkillPage();
                    skillPage.Dock = DockStyle.Fill;
                    Controls.Add(skillPage);
                    skillPage.BoundObject = skillTemplate;
                    skillPage.BindTemplateObject(contentObject);
                    break;
                default:
                    throw new Exception("Please implement this given content type.");
            }

            TabText = "[" + contentType + "] " + genericTemplate.Name;
            Text = "[" + contentType + "] " + genericTemplate.Name;
            Update();
            Invalidate();

        }

        private void GenericContentBindForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        ReleaseContent();
        }

        private void ReleaseContent()
        {
                var releaseRequest = new ContentReleasePacket(_contentType, _id);
                NetworkManager.Instance.SendPacket(releaseRequest);
            
        }


        public void Save()
        {
            var request = new ContentSaveRequestPacket(_bound as IContentTemplate, _contentType);

            // Send the request
            NetworkManager.Instance.SendPacket(request);


        }

    }
}
