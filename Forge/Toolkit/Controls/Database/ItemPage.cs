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

        public IContentTemplate BoundObject { get; set; }

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
            // Bind all the properties required

            // Textbox bindings
            textName.DataBindings.Add("Text", BoundObject, "Name");
            textDescription.DataBindings.Add("Text", BoundObject, "Description");

            // Numeric stuff will be validated here
            numericPrice.DataBindings.Add("Value", BoundObject, "Price");
            numericUseSpeed.DataBindings.Add("Value", BoundObject, "UseSpeed");

            // Checkboxes
            checkConsume.DataBindings.Add("Checked", BoundObject, "Consumed");

            // Bind the combobox we need
            comboItemType.DataSource = Enum.GetValues(typeof(ItemType)).Cast<ItemType>();
            comboItemType.DataBindings.Add("SelectedItem", BoundObject, "Type");


            // Enable this object
            Enabled = true;

        }

  

    }
}
