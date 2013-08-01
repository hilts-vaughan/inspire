using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public EditorTemplateEntry(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return "[" + ID + "] " +  Name;
        }

    }
}
