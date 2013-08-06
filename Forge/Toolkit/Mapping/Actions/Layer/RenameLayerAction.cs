using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions.Layer
{
    public class RenameLayerAction : IMapAction
    {
        private string _previousName;
        private string _newName;
        private int _layerID;


        public RenameLayerAction(string newName, int layerID)
        {
            _newName = newName;
            _layerID = layerID;
        }

        public string ActionName
        {
            get { return "Rename layer"; }
        }

        public void Execute(GameMap gameMap)
        {
            _previousName = gameMap.Layers[_layerID].Name;
            gameMap.Layers[_layerID].Name = _newName;
        }

        public void UnExecute(GameMap gameMap)
        {
            gameMap.Layers[_layerID].Name = _previousName;
        }


    }
}
