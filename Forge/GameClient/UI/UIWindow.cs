using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomiumUiLib;

namespace GameClient.UI
{
    /// <summary>
    /// A UiWindow loads an HTML fragment and binds itself into 
    /// </summary>
    public abstract class UiWindow
    {

        protected string _windowPath;

        public AwesomiumUI UiManager { get; set; }

        protected UiWindow(string windowPath)
        {
            _windowPath = windowPath;
            Html = File.ReadAllText(windowPath);
        }

        public string Html { get; private set; }


        public abstract void InjectJavascript();

    }
}
