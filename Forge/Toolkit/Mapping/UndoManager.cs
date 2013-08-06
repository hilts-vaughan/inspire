using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Mapping
{
    /// <summary>
    /// The undo manager allows the map to perform and unperform actions on itself that were done in the past
    /// It provides a clean way of managing these states.
    /// </summary>
    public class UndoManager
    {
        private MapTransactionMananger _transactionMananger;
        
        // Two stacks for redo and undo
        private Stack<IMapAction> _undo = new Stack<IMapAction>();
        private Stack<IMapAction> _redo = new Stack<IMapAction>(); 

        public UndoManager(MapTransactionMananger transactionMananger)
        {
            _transactionMananger = transactionMananger;
        }

        public int UndosLeft
        {
            get { return _undo.Count; }
        }

        public int RedosLeft
        {
            get { return _redo.Count; }
        }

        /// <summary>
        /// Adds a map action to the manager
        /// </summary>
        /// <param name="mapAction"></param>
        public void AddTransaction(IMapAction mapAction)
        {
            _undo.Push(mapAction);
            _redo.Clear();
        }

        public EventHandler UndoPerformed;
        public EventHandler RedoPeformed;

        public void PerformUndo()
        {           
            var lastAction = _undo.Pop();
            _redo.Push(lastAction);
            _transactionMananger.UnperformMapTransaction(lastAction);
            UndoPerformed(this, null);
        }

        public void PerformRedo()
        {
            var action = _redo.Pop();
            _undo.Push(action);
            _transactionMananger.PerformMapTransactionNoEvent(action);
            RedoPeformed(this, null);
        }

    }
}
