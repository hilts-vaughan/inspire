using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Toolkit
{
    /// <summary>
    /// A singelton manager that loads data files and maintains a listing of them all
    /// </summary>
    public class DataManager
    {
        private static DataManager _instance;

        /// <summary>
        /// The instance name qualifier of this manager
        /// </summary>
        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataManager();
                return _instance;
            }
        }

        private const int DATA_MAX = 255;

        public DataManager()
        {

        }

 

        /// <summary>
        /// Call this method to get the most up-to-date data all across the board
        /// </summary>
        public void Load()
        {
            //Load up all our tilesets
            LoadTilesets();

        }

        private void LoadTilesets()
        {



        }


    }
}
