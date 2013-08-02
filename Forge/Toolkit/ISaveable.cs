using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    /// <summary>
    /// A particular form that has something that can be saved can implement this function to interact with the main GUI
    /// </summary>
    public interface ISaveable
    {

        /// <summary>
        /// This method is called when main interface has recieved a save request
        /// </summary>
        void Save();

    }
}
