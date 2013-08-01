using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Enums;

namespace Toolkit.Controls.Database
{
    public interface IContentPage
    {

        /// <summary>
        /// The content type specified by this page
        /// </summary>
        ContentType ContentType { get; }

        /// <summary>
        /// The object that has been bound to this content page
        /// </summary>
        object BoundObject { get; set; }

        /// <summary>
        /// A method to begin binding the given object to the page itself
        /// </summary>
        /// <param name="templateObject"></param>
        void BindTemplateObject(object templateObject);

    }
}
