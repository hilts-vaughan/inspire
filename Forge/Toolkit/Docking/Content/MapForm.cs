using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlastersShared;
using Inspire.GameEngine.ScreenManager.Network;
using Inspire.Network.Packets.Client.Content;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Map;
using Inspire.Shared.Models.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toolkit.Mapping;
using Toolkit.Mapping.Actions;
using ButtonState = System.Windows.Forms.ButtonState;

namespace Toolkit.Docking.Content
{
    public partial class MapForm : ToolWindow, ISaveable
    {

        /// <summary>
        /// The transaction manager is an object in which all classes should try and interact with a map via
        /// </summary>
        public MapTransactionMananger TransactionMananger { get; set; }

        public UndoManager UndoManager { get; set; }
              

        /// <summary>
        /// The current layer
        /// </summary>
        public int CurrentLayer { get; set; }

        public MapForm()
        {
            InitializeComponent();

            Application.Idle += Application_Idle;

            // Create a backup stack that can go thirty items into the past
            BackupStack = new Stack<GameMapSnapshot>(30);
            RedoStack = new Stack<GameMapSnapshot>(30);



        }

        private void TransactionPerformed(object sender, MapTransactionMananger.TransactionEventArgs transactionEventArgs)
        {
            // Make sure it's not a one click tool, if so we should filter it out
            if (transactionEventArgs.ActionPerformed as GenericToolAction == null)
            {
                UndoManager.AddTransaction(transactionEventArgs.ActionPerformed);
            }
        }

        private DateTime then = DateTime.Now;

        void Application_Idle(object sender, EventArgs e)
        {
            if ((DateTime.Now - then).Milliseconds > 100)
            {
                then = DateTime.Now;
                mapView.SetMap(Map);
                Refresh();
            }
        }

        private MapTemplate _template;

        public GameMap Map { get; set; }

        public Stack<GameMapSnapshot> BackupStack { get; set; }
        public Stack<GameMapSnapshot> RedoStack { get; set; }

        public void SetBinding(object contentObject)
        {



            var genericTemplate = contentObject as IContentTemplate;
            _template = contentObject as MapTemplate;


            TabText = "[" + "Map" + "] " + genericTemplate.Name;
            Text = "[" + "Map" + "] " + genericTemplate.Name;
            Update();
            Invalidate();



            // Get our map
            var map = GameMap.FromTemplate(_template);
            Map = map;

            mapView.SetMap(map);

            TransactionMananger = new MapTransactionMananger(Map);
            TransactionMananger.TransactionPerformed += TransactionPerformed;
            TransactionMananger.TransactionUnperformed += TransactionUnperformed;
            UndoManager = new UndoManager(TransactionMananger);

        }

        private void TransactionUnperformed(object sender, MapTransactionMananger.TransactionEventArgs transactionEventArgs)
        {
            
        }

        public void TryToMakeContext()
        {
            mapView.TryToMakeContext();
        }

        public void Save()
        {
            // Serialize the data
            _template.BinaryData = SerializationHelper.ObjectToByteArray(Map);

            var request = new ContentSaveRequestPacket(_template, ContentType.Map);

            // Send the request
            NetworkManager.Instance.SendPacket(request);
        }

        private void timerRedraw_Tick(object sender, EventArgs e)
        {

        }

        private void mapView_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse.WindowHandle = mapView.Handle;
            var mouseState = Mouse.GetState();

            var prevX = -1;
            var prevY = -1;



            //var snapshot = (GameMap) SerializationHelper.ByteArrayToObject(SerializationHelper.ObjectToByteArray(Map));
            //BackupStack.Push(new GameMapSnapshot(snapshot,  MapEditorGlobals.ActiveActionType));
            var _transactionsToQueue = new List<IMapAction>();


            while (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {

                mouseState = Mouse.GetState();

                var x = mouseState.X / 32;
                var y = mouseState.Y / 32;

                if(prevX == x && prevY == y)
                    continue;

                if (x < 0 || y < 0)
                    continue;

                prevX = x;
                prevY = y;


                object[] args = {x, y, CurrentLayer, MapEditorGlobals.RectangleSelectedTiles};            
                var currentTool = MapEditorGlobals.ActiveActionType;
                var action = Activator.CreateInstance(currentTool, args);
                
                TransactionMananger.PerformMapTransaction(action as IMapAction);
                _transactionsToQueue.Add(action as IMapAction);


                // Refresh and paginate

                Refresh();
                Application.DoEvents();

                if (action.GetType() == typeof(FloodToolAction))
                    return;

                

            }

            // Create our package and feed it to the undo manager
            //_transactionsToQueue.Reverse();
            var package = new MapActionPackage(_transactionsToQueue);
            UndoManager.AddTransaction(package);

        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseContent();
        }

        private void ReleaseContent()
        {
            var releaseRequest = new ContentReleasePacket(ContentType.Map, _template.Id);
            NetworkManager.Instance.SendPacket(releaseRequest);
        }
    }
}
