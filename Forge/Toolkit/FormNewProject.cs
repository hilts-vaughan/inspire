using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class FormNewProject : Form
    {
        public FormNewProject()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = dialogBrowseLocation.ShowDialog();

            if (result == DialogResult.OK)
                textBoxLocation.Text = dialogBrowseLocation.SelectedPath;

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
            
            //Verify name is legal, may add more validation
            if (textBoxName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid name for this project!", "Project creation error",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //Verify if location is legal, may add more validation
            if (textBoxLocation.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid location for this project!", "Project creation error",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Check to see if a project already exists here and warn user
            if(File.Exists(textBoxLocation.Text + "\\project.dreamproj"))
            {
                DialogResult result = MessageBox.Show(
                    "A project already exists at this location - are you sure you want to overwrite it? This may cause corruption!", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;
            }

            CreateProject();

            DialogResult = DialogResult.OK;
            Close();
               
        }

        private void CreateProject()
        {
            ProjectSettings.Instance.Reset();

            //Set the project settings
            ProjectSettings.Instance.Name = textBoxName.Text;
            ProjectSettings.Instance.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ProjectSettings.Instance.Location = textBoxLocation.Text;

            //Save our project
            ProjectSettings.Instance.SaveProject();

            //Create our basic directories
            Directory.CreateDirectory(textBoxLocation.Text + "\\plugins");
            Directory.CreateDirectory(textBoxLocation.Text + "\\misc");

            //Create our content directories
            Directory.CreateDirectory(textBoxLocation.Text + "\\Content");
            Directory.CreateDirectory(textBoxLocation.Text + "\\Content\\Audio");
            Directory.CreateDirectory(textBoxLocation.Text + "\\Content\\Audio\\Music");
            Directory.CreateDirectory(textBoxLocation.Text + "\\Content\\Audio\\SFX");
            Directory.CreateDirectory(textBoxLocation.Text + "\\Content\\Graphics\\Tilesets");
            Directory.CreateDirectory(textBoxLocation.Text + "\\Content\\Misc");

            //Copy over our standard resources if needed


        }

    }
}
