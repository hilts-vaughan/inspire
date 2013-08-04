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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Toolkit.Mapping;
using C3.XNA;

namespace Toolkit.Controls.Rendering
{
    public partial class TilesetRenderControl : GraphicsDeviceControl
    {


        public TilesetRenderControl()
        {
            InitializeComponent();
        }

        protected override void Initialize()
        {
  
        }

        protected override void Draw()
        {

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            if (MapEditorGlobals.CurrentActiveTexture == null)
                return;



            var spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(MapEditorGlobals.CurrentActiveTexture, new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);

            spriteBatch.DrawRectangle(MapEditorGlobals.RectangleSelectedTiles, Microsoft.Xna.Framework.Color.Yellow, 4f);

            spriteBatch.End();

        }
     

    }
}
