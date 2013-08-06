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
            _transactionPackage.Reverse();
            foreach (var action in _transactionPackage)
                action.Execute(gameMap);


        }

        public void UnExecute(GameMap gameMap)
        {
            _transactionPackage.Reverse();
            foreach (var action in _transactionPackage)
                action.UnExecute(gameMap);
        }


    }
}
