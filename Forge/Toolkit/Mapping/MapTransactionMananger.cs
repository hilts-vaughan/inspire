using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Mapping
{
    /// <summary>
    /// The transaction manager helps manage things like state being managed on the map.
    /// This is used to modify state on the map. All map transactions should happen through
    /// this manager.
    /// </summary>
    public class MapTransactionMananger
    {

        /// <summary>
        /// This method allows a map action to be applied to the current transaction.
        /// </summary>
        public void PerformMapAction()
        {
            
        }

        public void UndoMapAction()
        {
            
        }

        public void RedoMapAction()
        {
            
        }

    }
}
