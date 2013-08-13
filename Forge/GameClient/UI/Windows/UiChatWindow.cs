using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium.Core;

namespace GameClient.UI.Windows
{
    public class UiChatWindow : UiWindow
    {
        public UiChatWindow() : base("Content\\UI\\windows\\chat.html")
        {

        }

        public override void InjectJavascript()
        {
            // Bind events we'll need
            JSObject loginActions = UiManager.webView.CreateGlobalJavascriptObject("chat");
            loginActions.Bind("submitMessage", false, SubmitMessage);

        }

        private void SubmitMessage(object sender, JavascriptMethodEventArgs e)
        {
            var statement = "$('#chatbuffer').append('" + "Soemthing, something darkside." + "\\n');";
            UiManager.CallJavascript(statement);
        }


    }
}
