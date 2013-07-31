using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit
{
    /// <summary>
    /// An interface that describes a form that maintains content
    /// </summary>
    public interface IContent
    {
        /// <summary>
        /// The content filename and path
        /// </summary>
        string ContentFilename { get; set; }

        /// <summary>
        /// Saves content 
        /// </summary>
        void SaveContent();


    }
}
