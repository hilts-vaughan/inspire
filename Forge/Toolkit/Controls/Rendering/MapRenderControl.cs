﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inspire.GameEngine;
using Inspire.GameEngine.ScreenManager;
using Inspire.Shared.Models.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Toolkit.Mapping;
using C3.XNA;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Toolkit.Controls.Rendering
{
    public partial class MapRenderControl : GraphicsDeviceControl
    {
        private ScreenManager _screenManager;
        private GameMap _gameMap;
        public MapEditScreen screen;
        private Color _gridColor;
        private SpriteBatch _sb;

        public Camera2D Camera
        {
            get { return screen.Camera2D; }
        }

        /// <summary>
        /// The rectangle selection
        /// </summary>
        public Rectangle SelectionRectangle { get; set; }

        public void SetMap(GameMap map)
        {


            _gameMap = map;

            if (screen != null)
            {
                screen.GameMap = map;
                screen._renderer._gameMap = map;
            }
        }

        public MapRenderControl()
        {
            InitializeComponent();

            _gridColor = Color.White;
            _gridColor.A = 15;



        }



        public void TryToMakeContext()
        {
            // Create our state manager and let's get started
            if (_screenManager == null)
            {
                _screenManager = new ScreenManager(null, GraphicsDevice, null);

                screen = new MapEditScreen(_gameMap);
                _screenManager.AddScreen(screen, null);
                screen.LoadContent();

                screen.Camera2D = new Camera2D(new Vector2(Width, Height), _gameMap.Layers[0].Width, _gameMap.Layers[1].Height, 1f);

                Resize += MapRenderControl_Resize;

                _sb = new SpriteBatch(GraphicsDevice);

            }

            // Since there was a context swap, do it up
            MapEditorGlobals.CurrentActiveTexture = screen._renderer._tilesetTexture;
            //CurrentActiveTexture
        }

        void MapRenderControl_Resize(object sender, EventArgs e)
        {
           screen.Camera2D.ViewportSize = new Vector2(Width, Height);
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


            _sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null,  Camera.GetTransformation());

            // Draw our grid overlay
            for (int x = 0; x < _gameMap.Layers[0].Width; x++)
            {
                for (int y = 0; y < _gameMap.Layers[0].Height; y++)
                {
                    var rect = new Rectangle(x * 32, y * 32, 32, 32);

                    _sb.DrawRectangle(rect, _gridColor, 2f);
                }
            }

            // Draw our selection
            _sb.DrawRectangle(SelectionRectangle, Color.White, 3f);

            _sb.End();


        }
    }
}
