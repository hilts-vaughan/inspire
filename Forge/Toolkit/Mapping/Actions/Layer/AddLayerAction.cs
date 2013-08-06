using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions.Layer
{
    public class AddLayerAction : IMapAction
    {

        private MapLayer _layerToAdd;
        private int _position;

        public AddLayerAction(MapLayer layerToAdd, int position)
        {
            _layerToAdd = layerToAdd;
            _position = position;
        }

        public string ActionName
        {
            get { return "Added layer"; }

        }

        public void Execute(GameMap gameMap)
        {
            gameMap.Layers.Insert(_position, _layerToAdd);
        }

        public void UnExecute(GameMap gameMap)
        {
            gameMap.Layers.Remove(_layerToAdd);
        }
    }
}
