using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Enums;

namespace Inspire.Shared
{
    /// <summary>
    /// An item template
    /// </summary>
    public class ItemTemplate
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
        public int Price { get; set; }
        public bool Consumed { get; set; }
        
        /// <summary>
        /// The amount of time it takes for this item to be used (millisecondss)
        /// </summary>
        public int UseSpeed { get; set; }

        

    }
}
