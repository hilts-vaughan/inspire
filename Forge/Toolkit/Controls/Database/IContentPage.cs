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
        /// The content type this content page will have
        /// </summary>
        ContentType ContentType { get; set; }



    }
}
