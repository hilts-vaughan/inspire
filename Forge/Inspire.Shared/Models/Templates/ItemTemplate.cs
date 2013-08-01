using System;
using Inspire.Shared.Models.Enums;

namespace Inspire.Shared.Models.Templates
{
    /// <summary>
    /// An item template
    /// </summary>
    public class ItemTemplate
    {
        public ItemTemplate(int id, string name, string description, ItemType type, int price, bool consumed, int useSpeed)
        {
            ID = id;
            Name = name;
            Description = description;
            Type = type;
            Price = price;
            Consumed = consumed;
            UseSpeed = useSpeed;
        }

        public ItemTemplate()
        {
            
        }

        /// <summary>
        /// A unique ID in the database
        /// </summary>
        public int ID { get; set; }

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
