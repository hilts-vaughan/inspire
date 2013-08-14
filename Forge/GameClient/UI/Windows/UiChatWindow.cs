using System;
using System.Collections.Generic;
using System.Linq;
using Awesomium.Core;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.Network.Packets.All;
using Inspire.Shared.Models.Enums;

namespace GameClient.UI.Windows
{
    public class UiChatWindow : UiWindow
    {
        public UiChatWindow()
            : base("Content\\UI\\windows\\chat.html")
        {
            PacketService.RegisterPacket<ChatPacket>(HandleChatPacket);
        }

        private void HandleChatPacket(ChatPacket chatPacket)
        {
            AppendText(chatPacket.Message);
        }

        public override void InjectJavascript()
        {
            // Bind events we'll need
            JSObject loginActions = UiManager.webView.CreateGlobalJavascriptObject("chat");
            loginActions.Bind("submitMessage", false, SubmitMessage);

        }

        private void SubmitMessage(object sender, JavascriptMethodEventArgs e)
        {
            var chatText = UiManager.webView.ExecuteJavascriptWithResult("document.getElementById('txt-chattext').value");
            UiManager.webView.ExecuteJavascript("document.getElementById('txt-chattext').value = '';");

            var packet = new ChatPacket(chatText, ChatChannel.Map);
            NetworkManager.Instance.SendPacket(packet);
        }

        private void AppendText(string chatText)
        {
            var statement = "$('#chatbuffer').append('" + chatText + "\\n');";
            UiManager.CallJavascript(statement);
        }



    }
}
