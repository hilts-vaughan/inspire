using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Shared.Models.Templates
{
    /// <summary>
    /// A map template contains information about a particular map and zone in the game world.
    /// </summary>
    public class MapTemplate : IContentTemplate
    {
        public MapTemplate(int id, string name, string virtualCategory)
        {
            Id = id;
            Name = name;
            VirtualCategory = virtualCategory;
        }

        public MapTemplate()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string VirtualCategory { get; set; }

        public byte ByteMe { get; set; }

        public byte[] BinaryData { get; set; }


    }
}
