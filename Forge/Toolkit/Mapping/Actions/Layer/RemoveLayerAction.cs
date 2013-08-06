using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions.Layer
{
    public class RemoveLayerAction : IMapAction
    {
        private MapLayer _layerToAdd;
        private int _previousIndex;

        public RemoveLayerAction(MapLayer layerToAdd)
        {
            _layerToAdd = layerToAdd;
        }

        public string ActionName
        {
            get { return "Removed layer"; }

        }

        public void Execute(GameMap gameMap)
        {
            _previousIndex = gameMap.Layers.IndexOf(_layerToAdd);
            gameMap.Layers.Remove(_layerToAdd);
        }

        public void UnExecute(GameMap gameMap)
        {
            gameMap.Layers.Insert(_previousIndex, _layerToAdd);
        }


    }
}
