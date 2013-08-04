using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.GameEngine.ScreenManager;
using Inspire.Shared.Models.Map;
using Toolkit.Mapping;

namespace Toolkit.Controls.Rendering
{
    public partial class MapRenderControl : GraphicsDeviceControl
    {
        private ScreenManager _screenManager;
        private GameMap _gameMap;

        public void SetMap(GameMap map)
        {


            _gameMap = map;

        }

        public MapRenderControl()
        {
            InitializeComponent();



        }

        public void TryToMakeContext()
        {
            // Create our state manager and let's get started
            if (_screenManager == null)
            {
                _screenManager = new ScreenManager(null, GraphicsDevice);

                var screen = new MapEditScreen(_gameMap);
                _screenManager.AddScreen(screen, null);
                screen.LoadContent();

            }
        }


        protected override void Initialize()
        {


        }

        protected override void Draw()
        {
            if (_screenManager == null)
                return;

            _screenManager.Draw(null);
            _screenManager.Update(null);
        }
    }
}
