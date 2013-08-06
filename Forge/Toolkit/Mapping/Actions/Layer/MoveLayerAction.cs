using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspire.Shared.Models.Map;

namespace Toolkit.Mapping.Actions.Layer
{
    public class MoveLayerAction : IMapAction
    {

        public enum LayerDirection
        {
            Up,
            Down
        }

        private LayerDirection _direction;
        private int _layerID;

        public MoveLayerAction(LayerDirection direction, int layerID)
        {
            _direction = direction;
            _layerID = layerID;
        }


        public string ActionName
        {
            get { return "Moved layer"; }
        }

        public void Execute(GameMap gameMap)
        {
            if (_direction == LayerDirection.Up)
                gameMap.Layers.Swap(_layerID, _layerID + 1);
            else
                gameMap.Layers.Swap(_layerID, _layerID - 1);



        }

        public void UnExecute(GameMap gameMap)
        {
            if (_direction == LayerDirection.Up)
                gameMap.Layers.Swap(_layerID, _layerID + 1);
            else
                gameMap.Layers.Swap(_layerID, _layerID - 1);
        }

    }
}
