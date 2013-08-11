using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium.Core;
using Inspire.GameEngine.ScreenManager;
using Inspire.GameEngine;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Server;
using Inspire.Shared.Crypto;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Screens
{
    /// <summary>
    /// A login screen is used to collect authorization credentials
    /// </summary>
    public class LoginScreen : GameScreen
    {
        private Texture2D _backdrop;

        public override void LoadContent()
        {
            // Load all the textures we need
            _backdrop = TextureLoader.GetTexture("bg_login.png", ScreenManager.GraphicsDevice);

            // Load this particular chrome
            UiManager.Load(@"Content\UI\login\index.html");
            UiManager.OnDocumentCompleted += OnDocumentCompleted;

            // Register network callbacks
            PacketService.RegisterPacket<LoginResultPacket>(HandleLoginResult);

            base.LoadContent();
        }

        private void HandleLoginResult(LoginResultPacket loginResultPacket)
        {
            if (loginResultPacket.Result == LoginResultPacket.LoginResult.Succesful)
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new GameplayScreen(), null);
            }
        }

        private void OnDocumentCompleted()
        {
            // Bind events we'll need
            JSObject loginActions = UiManager.webView.CreateGlobalJavascriptObject("login");
            loginActions.Bind("login", false, HandleLogin);
        }

        private void HandleLogin(object sender, JavascriptMethodEventArgs e)
        {
            var username = UiManager.webView.ExecuteJavascriptWithResult("document.getElementById('txt-username').value");
            var password = UiManager.webView.ExecuteJavascriptWithResult("document.getElementById('txt-password').value");
            password = HashHelper.CalculateSha512Hash(password);

            var packet = new LoginRequestPacket(username, password);
            NetworkManager.Instance.SendPacket(packet);

        }


        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {


            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(_backdrop, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);

        }


    }
}
