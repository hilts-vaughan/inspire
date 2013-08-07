using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions.Clipboard
{
    public class ClipboardCutMapAction : IMapAction
    {
        public string ActionName { get; private set; }
        public void Execute(GameMap gameMap)
        {
            throw new NotImplementedException();
        }

        public void UnExecute(GameMap gameMap)
        {
            throw new NotImplementedException();
        }
    }
}
