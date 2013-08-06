using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions.Layer
{
    public class ToggleLayerVisibility : IMapAction
    {

        private int _layerID;

        public ToggleLayerVisibility(int layerID)
        {
            _layerID = layerID;
        }

        public string ActionName
        {
            get { return "Toggled layer visibility"; }
        }
        public void Execute(GameMap gameMap)
        {
            gameMap.Layers[_layerID].Visible = !gameMap.Layers[_layerID].Visible;
        }

        public void UnExecute(GameMap gameMap)
        {
            gameMap.Layers[_layerID].Visible = !gameMap.Layers[_layerID].Visible;
        }
    }
}
