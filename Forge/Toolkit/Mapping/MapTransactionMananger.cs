using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping
{
    /// <summary>
    /// The transaction manager helps manage things like state being managed on the map.
    /// This is used to modify state on the map. All map transactions should happen through
    /// this manager.
    /// </summary>
    public class MapTransactionMananger
    {
        public class TransactionEventArgs : EventArgs
        {
            public IMapAction ActionPerformed { get; set; }

            public TransactionEventArgs(IMapAction actionPerformed)
            {
                ActionPerformed = actionPerformed;
            }
        }

        public EventHandler<TransactionEventArgs> TransactionPerformed;
        public EventHandler<TransactionEventArgs> TransactionUnperformed; 

        /// <summary>
        /// This is the game map this transaction manager will maintain
        /// </summary>
        private readonly GameMap _gameMap;

        public MapTransactionMananger(GameMap gameMap)
        {
            _gameMap = gameMap;
        }

        /// <summary>
        /// This method allows a map action to be applied to the current transaction.
        /// </summary>
        public void PerformMapTransaction(IMapAction mapAction)
        {
            mapAction.Execute(_gameMap);
            TransactionPerformed(this, new TransactionEventArgs(mapAction));
        }

        /// <summary>
        /// This method allows a map action to be applied to the current transaction.
        /// </summary>
        public void PerformMapTransactionNoEvent(IMapAction mapAction)
        {
            mapAction.Execute(_gameMap);
        }

        public void UnperformMapTransaction(IMapAction mapAction)
        {
            mapAction.UnExecute(_gameMap);
            TransactionUnperformed(this, new TransactionEventArgs(mapAction));
        }


    }
}
