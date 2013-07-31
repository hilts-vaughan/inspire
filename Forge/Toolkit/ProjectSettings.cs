using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Toolkit
{
    public class ProjectSettings
    {

        public ProjectSettings()
        {
            Assets = new AssetCollection();
        }

        private static ProjectSettings _instance;

        public void Reset()
        {
            _instance = new ProjectSettings();
        }

        /// <summary>
        /// The current instance of the project settings
        /// </summary>
        public static ProjectSettings Instance
        {
            get
            {
                if (_instance == null) _instance = new ProjectSettings();
                return _instance;
            }
        }


        /// <summary>
        /// The name of this project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A collection of assets in this project
        /// </summary>
        public AssetCollection Assets { get; set; }

        /// <summary>
        /// The version of this project file, used for comparison of projects.
        /// This will always be the same as the toolkit version!
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The location of this project on the disk, used internally only.
        /// </summary>
        [XmlIgnore]
        public string Location { get; set; }

        /// <summary>
        /// Saves the current project settings to the disk
        /// </summary>
        public void SaveProject()
        {
            using (FileStream fs = new FileStream(Location + "\\project.dreamproj", FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(GetType());
                serializer.Serialize(fs, this);
            }
        }


        public void LoadProject(string projectPath)
        {
            using (FileStream fs = new FileStream(projectPath + "\\project.dreamproj", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(GetType());
                _instance = (ProjectSettings)serializer.Deserialize(fs);
            }

            _instance.Location = projectPath;
        }

    }

    /// <summary>
    /// A collection of assets maintained in this project
    /// </summary>
    public class AssetCollection
    {
        //A list of internal assets for this project
        public List<Asset> MusicAssets { get; set; }

        public AssetCollection()
        {

        }        
    }

    public class Asset
    {
        /// <summary>
        /// The name of this asset
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location this asset is located in the asset folder
        /// </summary>
        public string DirectoryPath { get; set; }

        /// <summary>
        /// The type of asset this asset is
        /// </summary>
        public AssetType Type { get; set; }
    }

    /// <summary>
    /// An enumeration of various asset types
    /// </summary>
    public enum AssetType
    {
        Audio,
        Graphics,
        Misc
    }

}
