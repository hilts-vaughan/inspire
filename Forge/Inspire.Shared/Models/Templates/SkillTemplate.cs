using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Shared.Models.Templates
{
    /// <summary>
    /// A skill template represents a given skill in the database
    /// </summary>
    public class SkillTemplate : IContentTemplate
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string VirtualCategory { get; set; }
        public string Garbage { get; set; }

        public SkillTemplate(string description, int id, string name, string garbage)
        {
            Description = description;
            Id = id;
            Name = name;
            Garbage = garbage;
        }

        public SkillTemplate()
        {
            
        }

    }
}
