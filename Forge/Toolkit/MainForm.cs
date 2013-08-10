using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.Network;
using Inspire.Network.Packets.Client;
using Inspire.Network.Packets.Client.Content;
using Inspire.Network.Packets.Server;
using Inspire.Network.Packets.Server.Content;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Templates;
using Toolkit.Configuration;
using Toolkit.Docking;
using Toolkit.Docking.Content;
using Toolkit.Mapping;
using Toolkit.Mapping.Actions;
using WeifenLuo.WinFormsUI.Docking;
using Style = WeifenLuo.WinFormsUI.Docking.Skins.Style;

namespace Toolkit
{
    public partial class MainForm : Form
    {

        //Our windows we should use internally
        FormContentExplorer _contentExplorer = new FormContentExplorer();
        FormAssetExplorer _assetExplorer = new FormAssetExplorer();

        // All the dock windows being used
        ContentDockForm _contentDockForm = new ContentDockForm();
        HistoryDockForm _historyDockForm = new HistoryDockForm();
        LayersDockForm _layersDockForm = new LayersDockForm();
        TilesetDockForm _tilesetDockForm = new TilesetDockForm();

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;

        private Thread thread;

        // collection of button groups
        private readonly ReadOnlyCollection<ReadOnlyCollection<ToolStripButton>> mGroups;

        // an individual button group
        private readonly ReadOnlyCollection<ToolStripButton> mGroup;

        public MainForm()
        {
            CheckForIllegalCrossThreadCalls = false;

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            InitializeComponent();

            dockPanel.Theme = new VS2012LightTheme();
            //dockPanel.Theme = new VS2005Theme();


            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            // Bind for events
            _contentDockForm.ContentRequested += ContentRequested;

            // Listen for some particular events
            PacketService.RegisterPacket<ContentResultPacket>(Handler);
            PacketService.RegisterPacket<ContentSaveResultPacket>(HandleSaveResult);

            //RegisterHotkeys();
            dockPanel.ActiveDocumentChanged += DockPanelOnActiveDocumentChanged;


            // add controls to this list as needed
            mGroup = new List<ToolStripButton>()
                {
                buttonPencil,
                buttonEraser,
                buttonFill,
                buttonDropper
                }.AsReadOnly();

            // add new groups to this list as needed
            mGroups = new List<ReadOnlyCollection<ToolStripButton>>
                {
                   mGroup
                }.AsReadOnly();

        }

        private void DockPanelOnActiveDocumentChanged(object sender, EventArgs eventArgs)
        {
            var form = ((DockPanel)sender).ActiveDocument as MapForm;

            TryAndBindMap(form);
        }

        private void TryAndBindMap(MapForm form)
        {
            if (form != null)
            {
                form.TryToMakeContext();
                _layersDockForm.BindLayers(form);

            }
        }

        private void RegisterHotkeys()
        {
            Hotkey hk = new Hotkey();
            hk.Windows = true;

            hk.Pressed += HkOnPressed;
            hk.Register(this);

        }

        private void HkOnPressed(object sender, HandledEventArgs handledEventArgs)
        {

        }

        private void HandleSaveResult(ContentSaveResultPacket contentSaveResultPacket)
        {
            if (contentSaveResultPacket.RequestResult == RequestResult.Succesful)
            {
                //TODO: Notify the user it was successful
            }
            else
            {
                ShowMessageBox(
                    "The server rejected your save request. This usually happens due to trying to save locked content.",
                    "Server Response");
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S | Keys.ShiftKey))
            {
                buttonSaveContent.PerformClick();
                return true;
            }


            if (keyData == (Keys.Control | Keys.S))
            {
                buttonSaveContent.PerformClick();
                return true;
            }


            if (keyData == (Keys.Control | Keys.C))
            {
                copyButton.PerformClick();
                return true;
            }


            if (keyData == (Keys.Control | Keys.V))
            {
                pasteButton.PerformClick();
                return true;
            }



            if (keyData == (Keys.Control | Keys.X))
            {
                cutButton.PerformClick();
                return true;
            }


            if (keyData == (Keys.Control | Keys.Z))
            {
                buttonUndo.PerformClick();
                return true;
            }


            if (keyData == (Keys.Control | Keys.Y))
            {
                buttonRedo.PerformClick();
                return true;
            }


            if (keyData == (Keys.P))
            {
                buttonPencil.PerformClick();
                return true;
            }



            if (keyData == (Keys.D))
            {
                buttonDropper.PerformClick();
                return true;
            }


            if (keyData == (Keys.F))
            {
                buttonFill.PerformClick();
                return true;
            }


            if (keyData == (Keys.E))
            {
                buttonEraser.PerformClick();
                return true;
            }

            if (keyData == (Keys.Q))
            {
                _layersDockForm.LayerMoveUp();
            }

            if (keyData == (Keys.W))
            {
                _layersDockForm.LayerMoveDown();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        public DialogResult ShowMessageBox(String message, String caption)
        {
            if (this.InvokeRequired)
            {
                return (DialogResult)this.Invoke(new FormDatabase.PassStringStringReturnDialogResultDelegate(ShowMessageBox), message, caption);
            }
            return MessageBox.Show(this, message, caption);
        }

        private void Handler(ContentResultPacket obj)
        {
            _pendingNetworkRequest = false;

            if (obj.Locked)
            {
                ShowMessageBox("The server rejected your request for this content. This usually happens because someone else has it checked out.", "Server Response");
                return;
            }


            _garb = obj;

            if (obj.ContentType != ContentType.Map)
                ShowForm();
            else
                ShowMapForm();



        }

        private void ShowMapForm()
        {
            if (this.InvokeRequired)
            { // this refers to the current form
                this.Invoke(new Action(ShowMapForm));  // this line invokes the same function on the same thread as the current form
                return;
            }

            var bindForm = new MapForm();
            bindForm.SetBinding(_garb.ContentObject);
            bindForm.Show(dockPanel, DockState.Document);
        }





        private ContentResultPacket _garb;

        private void ShowForm()
        {
            if (this.InvokeRequired)
            { // this refers to the current form
                this.Invoke(new Action(ShowForm));  // this line invokes the same function on the same thread as the current form
                return;
            }

            var bindForm = new GenericContentBindForm();
            bindForm.Show(dockPanel, DockState.Document);
            bindForm.SetBinding(_garb.ContentObject, _garb.ContentType);
        }



        // This is a list of pending requests from the network
        private bool _pendingNetworkRequest = false;

        private void ContentRequested(object sender, TreeNodeMouseClickEventArgs treeNodeMouseClickEventArgs)
        {

            if (_pendingNetworkRequest)
            {
                MessageBox.Show("You must wait for your current network request to complete before issuing another.");
                return;
            }

            // Retrieve the entry
            var entry = (EditorTemplateEntry)treeNodeMouseClickEventArgs.Node.Tag;
            _pendingNetworkRequest = true;

            var request = new ContentRequestPacket(entry.ContentType, entry.ID);


            // Send the request
            NetworkManager.Instance.SendPacket(request);


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
            if (persistString == typeof(ContentDockForm).ToString())
                return _contentDockForm;

            if (persistString == typeof(HistoryDockForm).ToString())
                return _historyDockForm;

            if (persistString == typeof(LayersDockForm).ToString())
                return _layersDockForm;

            if (persistString == typeof(TilesetDockForm).ToString())
                return _tilesetDockForm;

            if (persistString == typeof(GenericContentBindForm).ToString())
                return null;
            if (persistString == typeof(MapForm).ToString())
                return null;

            throw new Exception("A backwards compatibilty issue was detected - regressing");
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

            //Set to the working directory
            ProjectSettings.Instance.Location = Directory.GetCurrentDirectory();

            ProjectSettings.Instance.SaveProject();
            Text = "Inspire - " + ProjectSettings.Instance.Name + "   [" + ProjectSettings.Instance.Location + "]";


            //    _contentExplorer.Show(dockPanel);
            //    _assetExplorer.Show(dockPanel);


            //Update the network manager
            thread = new Thread(NetworkLoop);
            thread.Start();


            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "windows.config");


            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);

            if (!_contentDockForm.Visible)
                _contentDockForm.Show(dockPanel, DockState.DockLeft);


            // Show the login dialog - need to get an authentication token before we can do anything interesting
            var loginForm = new FormLogin();
            loginForm.ShowDialog();

            // Show the waiting screen and ask for patience

            // Generate the ContentMap dynamically, assinging everyone a backing
            foreach (var contentType in GetValues<ContentType>())
            {
                //  Send out a request for each content type
                var request = new ContentListRequestPacket(contentType);
                NetworkManager.Instance.SendPacket(request);
            }

        }



        private static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }


        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "windows.config");
            if (m_bSaveLayout)
                dockPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);

            // Save out to the app config
            AppConfiguration.Instance.Serialize();
        }



        #endregion

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void MainForm_Shown(object sender, EventArgs e)
        {




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
            _contentDockForm.Show();
            //if (!_contentExplorer.IsHidden)
            //    _contentExplorer.Hide();
            //else
            //    _contentExplorer.Show();
        }

        private void assetExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _assetExplorer.Show(dockPanel);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }





        private void dockPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _historyDockForm.Show(dockPanel);
        }

        private void layersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _layersDockForm.Show(dockPanel);
        }

        private void tilesetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tilesetDockForm.Show(dockPanel);
        }

        private void MainForm_Leave(object sender, EventArgs e)
        {
            thread.Abort();
        }

        private void buttonSaveContent_Click(object sender, EventArgs e)
        {
            var activePanel = dockPanel.ActiveDocument;
            var saveable = activePanel as ISaveable;
            var x = 12;

            if (saveable != null)
            {
                saveable.Save();
            }

        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            foreach (var window in dockPanel.FloatWindows)
            {
                var saveable = window as ISaveable;
                if (saveable != null)
                {
                    saveable.Save();
                }
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            buttonSaveContent.PerformClick();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buttonSaveAll.PerformClick();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://neoindies.com/");
        }

        private void buttonPencil_Click(object sender, EventArgs e)
        {
            VerifySingleCheck(sender);
            MapEditorGlobals.ActiveActionType = typeof(PencilAction);
        }

        private void VerifySingleCheck(object sender)
        {
            foreach (ReadOnlyCollection<ToolStripButton> group in mGroups)
                if (@group.Contains(sender))
                    foreach (ToolStripButton b in @group)
                        b.Checked = b == sender;
        }

        private void buttonDropper_Click(object sender, EventArgs e)
        {
            VerifySingleCheck(sender);
        }

        private void buttonEraser_Click(object sender, EventArgs e)
        {
            MapEditorGlobals.ActiveActionType = typeof(EraserAction);
            VerifySingleCheck(sender);
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            MapEditorGlobals.ActiveActionType = typeof(FloodToolAction);
            VerifySingleCheck(sender);
        }



        public MapForm GetActiveMap()
        {
            return dockPanel.ActiveDocument as MapForm;
        }

        private void UpdateRedoAndUndo()
        {
            var map = GetActiveMap();
            var undosLeft = map.UndoManager;
        }


        private void buttonUndo_Click(object sender, EventArgs e)
        {

            var map = GetActiveMap();

            if (map != null)
            {

                if (map.UndoManager.UndosLeft == 0)
                {
                    MessageBox.Show("There's nothing left to undo.");
                    return;
                }

                map.UndoManager.PerformUndo();
                return;
                map.RedoStack.Push(new GameMapSnapshot(map.Map, typeof(PencilAction)));
                var backupState = map.BackupStack.Pop();
                map.Map = backupState.Map;
                TryAndBindMap(map);
            }
            else
            {
                MessageBox.Show("Please select a map before trying to perform map actions.");
            }

        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            var map = GetActiveMap();

            if (map != null)
            {

                if (map.UndoManager.RedosLeft == 0)
                {
                    MessageBox.Show("There's nothing left to redo.");
                    return;
                }

                map.UndoManager.PerformRedo();
                return;
                var state = map.RedoStack.Pop();
                var backupState = map.BackupStack.Pop();
                map.Map = backupState.Map;
                TryAndBindMap(map);
            }
            else
            {
                MessageBox.Show("Please select a map before trying to perform map actions.");
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var map = GetActiveMap();
            if (map != null)
                map.CutTiles();

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            var map = GetActiveMap();
            if (map != null)
                map.CopyCurrentMapToBuffer();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var map = GetActiveMap();
            if (map != null)
                map.PasteTiles();
        }




    }
}