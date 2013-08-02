using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.Shared;
using Inspire.Shared.Models;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;

namespace Toolkit.Controls.Database
{
    public partial class SkillPage : UserControl, IContentPage
    {


        public SkillPage()
        {
            InitializeComponent();

            // Don't enable this control until an object is bound
            Enabled = false;
        }


        public ContentType ContentType
        {
            get { return ContentType.Skill; }
        }

        public object BoundObject { get; set; }

        public void BindTemplateObject(object templateObject)
        {
            // var o = Convert.ChangeType(templateObject, ContentType);
            var o = templateObject as SkillTemplate;
            BindItem(o);
        }


        /// <summary>
        /// Binds a particular item to this control.
        /// </summary>
        /// <param name="skillTemplate"></param>
        public void BindItem(SkillTemplate skillTemplate)
        {
            // Bind all the properties required

            // Textbox bindings
            textName.DataBindings.Add("Text", BoundObject, "Name");
            textDescription.DataBindings.Add("Text", BoundObject, "Description");

            // Enable this object
            Enabled = true;

        }

  

    }
}
