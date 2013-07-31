using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Shared.Models
{

    /// <summary>
    /// A player character that exists within the game world
    /// </summary>
    public class Character
    {

        /// <summary>
        /// The unique ID associated with this character
        /// </summary>
        public int CharacterId { get; set; }

        /// <summary>
        /// The account ID this character is associated with
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// The name of this given character
        /// </summary>
        public string Name { get; set; }


    }
}
