using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models;
using Inspire.Shared.Service;

namespace GameServer
{
    public class ServerServiceContainer : ServiceContainer 
    {

        /// <summary>
        /// A list of characters that are logged in
        /// </summary>
        public List<Character> Characters { get; set; }


        public ServerServiceContainer()
        {
            Characters = new List<Character>();           
        }

    }
}
