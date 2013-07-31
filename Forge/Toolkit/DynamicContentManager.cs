using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Toolkit
{
    public class DynamicContentManager : ContentManager 
    {
        private string oldDirectory = "";

        public DynamicContentManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public DynamicContentManager(IServiceProvider serviceProvider, string rootDirectory) : base(serviceProvider, rootDirectory)
        {
        }

        public T DynamicLoad<T>(string assetName)
        {
            //Create our temporary file with the content builder
            ContentBuilder contentBuilder = new ContentBuilder();
            string assetPath = ProjectSettings.Instance.Location + "\\" + RootDirectory + "\\";


                contentBuilder.Add(assetPath + assetName, assetName, null, null);


            string output = contentBuilder.Build();



            //TODO: Songs don't import properly... something is screwy with them
            if(output != null) 
            {
                throw new Exception("The specified asset " + assetName + " could not be built. Incompatible file type?");
            }


            Guid guid = Guid.NewGuid();

            //Copy over to the directory
           // File.Copy(contentBuilder.OutputDirectory + "\\someAsset_" + assetName + ".xnb", "\\Content\\someAsset_" + guid + ".xnb", true);

            string filename = Path.GetFileName(assetName);

            Environment.CurrentDirectory = contentBuilder.OutputDirectory;

            var obj = Load<T>(assetName);

            //File.Delete("Content\\someAsset_" + guid + ".xnb");

            return obj;
        }

   
 

    }
}
