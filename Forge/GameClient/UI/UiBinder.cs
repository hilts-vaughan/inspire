using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomiumUiLib;

namespace GameClient.UI
{
    /// <summary>
    /// The UI binder will bind particular windows to the interface DOM
    /// </summary>
    public static class UiBinder
    {
        public static void AttachWindow(AwesomiumUI UiManager, UiWindow window)
        {
            var html = window.Html;
            var statement = "$('#wrapper').append('" + html + "');";
            UiManager.CallJavascript(statement);
            window.UiManager = UiManager;
            window.InjectJavascript();
        }

    }
}
