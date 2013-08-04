using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Toolkit
{
    public static class DirectoryHelper
    {
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        /// <summary>
        /// The directory to the graphics
        /// </summary>
        public static string GraphicsDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\graphics\\";

        /// <summary>
        /// The directory leading to the maps
        /// </summary>
        public static string MapsDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\maps\\";
    }
}
