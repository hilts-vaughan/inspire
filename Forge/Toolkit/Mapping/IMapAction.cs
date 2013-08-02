using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Mapping
{
    /// <summary>
    /// An interface to a map action 
    /// </summary>
    public interface IMapAction
    {

        /// <summary>
        /// The name of this action - as will be displayed in the history pane
        /// </summary>
        string ActionName { get; }

    }
}
