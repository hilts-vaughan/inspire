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
    public partial class ItemPage : UserControl, IContentPage
    {

        private ItemTemplate _boundObject;

        public ItemPage()
        {
            InitializeComponent();

            // Don't enable this control until an object is bound
            Enabled = false;
        }


        public ContentType ContentType
        {
            get { return ContentType.Item; }
        }        

        public void BindTemplateObject(object templateObject)
        {
            // var o = Convert.ChangeType(templateObject, ContentType);
            var o = templateObject as ItemTemplate;
            BindItem(o);
        }


        /// <summary>
        /// Binds a particular item to this control.
        /// </summary>
        /// <param name="itemTemplate"></param>
        public void BindItem(ItemTemplate itemTemplate)
        {
            // The bound template goes here
            _boundObject = itemTemplate;

            // Bind all the properties required

            // Textbox bindings
            textName.DataBindings.Add("Text", _boundObject, "Name");
            textDescription.DataBindings.Add("Text", _boundObject, "Description");

            // Numeric stuff will be validated here
            numericPrice.DataBindings.Add("Value", _boundObject, "Price");
            numericUseSpeed.DataBindings.Add("Value", _boundObject, "UseSpeed");

            // Checkboxes
            checkConsume.DataBindings.Add("Checked", _boundObject, "Consumed");

            // Enable this object
            Enabled = true;

        }

  

    }
}
