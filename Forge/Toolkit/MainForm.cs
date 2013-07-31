using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Toolkit.Mapping;
using Toolkit.Network;
using WeifenLuo.WinFormsUI.Docking;
using ScintillaNet;

namespace Toolkit
{
    public partial class MainForm : Form
    {

        //Our windows we should use internally
        FormContentExplorer _contentExplorer = new FormContentExplorer();
        FormAssetExplorer _assetExplorer = new FormAssetExplorer();

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;

        private Thread thread;

        public MainForm()
        {
            CheckForIllegalCrossThreadCalls = false;

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            InitializeComponent();



            //  m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            //Kill the network loop
            thread.Abort();
        }

        #region Methods

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }



        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                for (int index = dockPanel.Contents.Count - 1; index >= 0; index--)
                {
                    if (dockPanel.Contents[index] is IDockContent)
                    {
                        IDockContent content = (IDockContent)dockPanel.Contents[index];
                        content.DockHandler.Close();
                    }
                }
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {

            //else if (persistString == typeof(DummyTaskList).ToString())
            //  return m_taskList;

            return new DockContent();
        }

        private void CloseAllContents()
        {


            // Close all other document windows
            CloseAllDocuments();
        }







        #endregion

        #region Event Handlers



        private void MainForm_Load(object sender, System.EventArgs e)
        {

        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }



        #endregion

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

            ////Check if a project had been loaded previously
            //if (File.Exists(@"editor.config"))
            //{
            //    //The first line is the filepath, make sure it's still existent on the disk
            //    using (StreamReader reader = new StreamReader(@"editor.config"))
            //    {
            //        string path = reader.ReadLine();

            //        //If the project still exists...
            //        if (File.Exists(path + "\\project.dreamproj"))
            //        {
            //            ProjectSettings.Instance.LoadProject(path);
            //        }
            //        else
            //        {
            //            DialogResult result = MessageBox.Show(
            //                    "The project loaded cannot be found on the disk. Would you like to create a new project? Otherwise, you will be prompted to load one.",
            //                    "Project load error",
            //                    MessageBoxButtons.YesNo);

            //            if (result == DialogResult.Yes)
            //                CreateNewProject();
            //            else
            //            {
            //                //You really should do something...

            //            }

            //        }

            //    }
            //}

            //else
            //{
            //    //No existing project, prompt to make one
            //    CreateNewProject();
            //}

            //Set to the working directory
            ProjectSettings.Instance.Location = Directory.GetCurrentDirectory();

            ProjectSettings.Instance.SaveProject();
            Text = "Inspire - " + ProjectSettings.Instance.Name + "   [" + ProjectSettings.Instance.Location + "]";


            //    _contentExplorer.Show(dockPanel);
            //    _assetExplorer.Show(dockPanel);


            //Update the network manager
            thread = new Thread(NetworkLoop);
            thread.Start();

        }

        void NetworkLoop()
        {
            while (true)
            {
                NetworkManager.Instance.Update();
                Thread.Sleep(10);
            }
        }

        private void CreateNewProject()
        {
            FormNewProject form = new FormNewProject();
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                //This was the newest loaded project, update editor.config
                File.WriteAllText("editor.config", ProjectSettings.Instance.Location);
            }

            //If the user dosen't end up making a new project anyway, just reboot for now
            Application.Restart();

        }

        private void toolStripMenuItemNewProject_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Creating a new project will close the current one - continue?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                //Create the new project...
                CreateNewProject();
            }
        }

        private void etcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormScript().Show(dockPanel);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Tell the application it is time to terminate
            Application.Exit();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Start up the game engine
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\GameClient.exe");
            process.Start();


        }

        void process_Exited(object sender, EventArgs e)
        {
            MainMenuStrip.Enabled = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }



        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllDocuments();
        }

        private void contentExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_contentExplorer.IsHidden)
                _contentExplorer.Hide();
            else
                _contentExplorer.Show();
        }

        private void assetExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_assetExplorer.IsHidden)
                _assetExplorer.Hide();
            else
                _assetExplorer.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }





        private void dockPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                MessageBox.Show("!");
            }

        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
                MessageBox.Show("!");
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TODO: Implement...");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            var form = new FormDatabase();
            form.ShowDialog();
        }




    }
}