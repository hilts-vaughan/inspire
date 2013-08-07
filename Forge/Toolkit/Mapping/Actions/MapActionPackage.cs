using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions
{
    public class MapActionPackage : IMapAction
    {

        private readonly List<IMapAction> _transactionPackage;

        public MapActionPackage(List<IMapAction> transactionPackage)
        {
            _transactionPackage = transactionPackage;
        }

        /// <summary>
        /// The action name for a package is just the first element
        /// </summary>
        public string ActionName
        {
            get { return _transactionPackage[0].ActionName; }
        }


        public void Execute(GameMap gameMap)
        {
            for (int index = _transactionPackage.Count - 1; index >= 0; index--)
            {
                var action = _transactionPackage[index];
                action.Execute(gameMap);
            }
        }

        public void UnExecute(GameMap gameMap)
        {
            //_transactionPackage.Reverse();
           // _transactionPackage.Reverse();
            for (int index = 0; index < _transactionPackage.Count; index++)
            {
                var action = _transactionPackage[index];
                action.UnExecute(gameMap);
            }
        }
    }
}
