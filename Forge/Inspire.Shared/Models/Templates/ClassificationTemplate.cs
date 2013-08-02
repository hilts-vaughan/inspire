using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Shared.Models.Templates
{
    /// <summary>
    /// Provides a template for classifications available in the game
    /// </summary>
    public class ClassificationTemplate : IContentTemplate
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string VirtualCategory { get; set; }
    }
}
