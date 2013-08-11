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

        public int Level { get; set; }

        public int MapId { get; set; }
        public int WorldX { get; set; }
        public int WorldY { get; set; }


        public Character(int characterId, int accountId, string name, int level, int mapId, int worldX, int worldY)
        {
            CharacterId = characterId;
            AccountId = accountId;
            Name = name;
            Level = level;
            MapId = mapId;
            WorldX = worldX;
            WorldY = worldY;
        }
    }
}
