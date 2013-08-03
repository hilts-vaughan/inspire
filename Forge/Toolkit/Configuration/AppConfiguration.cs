using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Toolkit.Configuration
{
    /// &lt;summary&gt;
    /// Summary description for ConfigurationSettings.
    /// &lt;/summary&gt;
    [Serializable]
    public class AppConfiguration
    {
        private static AppConfiguration _instance;
        private string _path = "settings.xml";

        public AppConfiguration()
        {
            // Create a unique directory just for these settings

            // The folder for the roaming current user 
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(folder, "InspireToolkit");

            // Check if folder exists and if not, create it
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            _path = specificFolder + "\\" + _path;

        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
 

        public static AppConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppConfiguration();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public void Serialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppConfiguration));
            TextWriter tw = new StreamWriter(_path);
            serializer.Serialize(tw, _instance);
            tw.Close();
        }

        public void Deserialize()
        {
            if(!File.Exists(_path))
                Serialize();

            XmlSerializer serializer = new XmlSerializer(typeof(AppConfiguration));
            TextReader tr = new StreamReader(_path);
            Instance = (AppConfiguration)serializer.Deserialize(tr);
            tr.Close();
        }
    }
}
