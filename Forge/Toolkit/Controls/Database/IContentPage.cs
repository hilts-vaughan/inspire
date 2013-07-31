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
        /// The content type this content page will have for static typing
        /// </summary>
        Type ContentType { get; }

        /// <summary>
        /// A method to begin binding the given object to the page itself
        /// </summary>
        /// <param name="templateObject"></param>
        void BindTemplateObject(object templateObject);

    }
}
