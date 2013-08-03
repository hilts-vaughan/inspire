using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;
using Toolkit.Controls.Database;

namespace Toolkit.Docking.Content
{
    public partial class GenericContentBindForm : ToolWindow
    {
        public GenericContentBindForm()
        {
            InitializeComponent();

            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }

        }


        public void SetBinding(object contentObject, ContentType contentType)
        {
            var genericTemplate = contentObject as IContentTemplate;

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


    }
}
