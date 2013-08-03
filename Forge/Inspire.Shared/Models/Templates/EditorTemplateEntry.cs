using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Enums;

namespace Inspire.Shared.Models.Templates
{
    /// <summary>
    /// An entry in the editor that is displayed to the user. This encapsulates things like name and ID.
    /// </summary>
    public class EditorTemplateEntry
    {

        public EditorTemplateEntry()
        {
            
        }

        public EditorTemplateEntry(int id, string name, string virtualDirectory, ContentType contentType)
        {
            ID = id;
            Name = name;
            VirtualDirectory = virtualDirectory;
            ContentType = contentType;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string VirtualDirectory { get; set; }
        public ContentType ContentType { get; set; }

        public override string ToString()
        {
            return "[" + ID + "] " +  Name;
        }

    }
}
