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
using Inspire.Shared;
using Inspire.Shared.Models.Enums;
using Inspire.Shared.Models.Map;
using Inspire.Shared.Models.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Toolkit.Mapping;
using Toolkit.Mapping.Actions;
using Toolkit.Mapping.Actions.Clipboard;
using ButtonState = System.Windows.Forms.ButtonState;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Toolkit.Docking.Content
{
    public partial class MapForm : ToolWindow, ISaveable
    {

        /// <summary>
        /// The transaction manager is an object in which all classes should try and interact with a map via
        /// </summary>
        public MapTransactionMananger TransactionMananger { get; set; }

        public UndoManager UndoManager { get; set; }


        private Rectangle _mapSelection;

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

        private bool _hasCopied = false;

        public void CopyCurrentMapToBuffer()
        {
            if (mapView.SelectionRectangle == Rectangle.Empty)
                return;


            _hasCopied = true;
            Rectangle selected = mapView.SelectionRectangle;
            var x = selected.X / 32;
            var y = selected.Y / 32;

            MapEditorGlobals.GlobalClipboardBuffer = new int[selected.Width / 32, selected.Height / 32];


            // We need to loop over the width of the editor
            for (int w = 0; w < selected.Width / 32; w++)
            {
                for (int h = 0; h < selected.Height / 32; h++)
                {
                    MapEditorGlobals.GlobalClipboardBuffer[w, h] =
                        Map.Layers[CurrentLayer].MapTiles[x + w][y + h].TileId;

                }
            }



        }

        public void CutTiles()
        {
            if (mapView.SelectionRectangle == Rectangle.Empty)
                return;


            CopyCurrentMapToBuffer();
            var action = new ClipboardCutMapAction(mapView.SelectionRectangle, CurrentLayer,
                                                   MapEditorGlobals.GlobalClipboardBuffer);
            TransactionMananger.PerformMapTransaction(action);
            mapView.SelectionRectangle = Rectangle.Empty;
        }

        public void PasteTiles()
        {
            if (mapView.SelectionRectangle == Rectangle.Empty)
            {
                MessageBox.Show("Select a location to paste first.");
                return;
            }



            if (!_hasCopied)
                return;

            // Apply the transaction
            var action = new ClipboardPasteMapAction(mapView.SelectionRectangle, CurrentLayer,
                                                     MapEditorGlobals.GlobalClipboardBuffer);
            TransactionMananger.PerformMapTransaction(action);


            mapView.SelectionRectangle = Rectangle.Empty;
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

            if (e.Button == MouseButtons.Right)
            {
                mapView.SelectionRectangle = new Rectangle(e.X / 32 * 32, e.Y / 32 * 32, 32, 32);
            }

            if (e.Button == MouseButtons.Left)
            {
                Mouse.WindowHandle = mapView.Handle;
                var mouseState = Mouse.GetState();

                var prevX = -1;
                var prevY = -1;



                //var snapshot = (GameMap) SerializationHelper.ByteArrayToObject(SerializationHelper.ObjectToByteArray(Map));

                //BackupStack.Push(new GameMapSnapshot(snapshot,  MapEditorGlobals.ActiveActionType));
                var _transactionsToQueue = new Stack<IMapAction>();


                while (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {

                    mouseState = Mouse.GetState();

                    var worldPosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(mapView.Camera.GetTransformation()));


                    var x = (int) (worldPosition.X / 32);
                    var y = (int) (worldPosition.Y / 32);

                    if (prevX == x && prevY == y)
                        continue;

                    if (x < 0 || y < 0)
                        continue;

                    prevX = x;
                    prevY = y;


                    object[] args = { x, y, CurrentLayer, MapEditorGlobals.RectangleSelectedTiles };
                    var currentTool = MapEditorGlobals.ActiveActionType;
                    var action = Activator.CreateInstance(currentTool, args);

                    TransactionMananger.PerformMapTransaction(action as IMapAction);
                    // UndoManager.AddTransaction(action as IMapAction);
                    _transactionsToQueue.Push(action as IMapAction);



                    // Refresh and paginate

                    Refresh();
                    Application.DoEvents();

                    if (action.GetType() == typeof(FloodToolAction))
                        break;



                }




                // Create our package and feed it to the undo manager
                _transactionsToQueue.Reverse();
                var package = new MapActionPackage(_transactionsToQueue.ToList());
                UndoManager.AddTransaction(package);
            }


            // Do right click stuff
            if (e.Button == MouseButtons.Right)
            {

                //TODO: Implement me
                return;
            }



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

        private void mapView_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                if (mapView.SelectionRectangle.Width < 32)
                    mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X,
                                                               mapView.SelectionRectangle.Y, 32,
                                                               mapView.SelectionRectangle.Height);

                if (mapView.SelectionRectangle.Height < 32)
                    mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X,
                                                               mapView.SelectionRectangle.Y, mapView.SelectionRectangle.Width,
                                                               32);

                if (e.X < mapView.SelectionRectangle.X || e.Y < mapView.SelectionRectangle.Y)
                    return;


                mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X, mapView.SelectionRectangle.Y, (int)Math.Ceiling((e.X - mapView.SelectionRectangle.X) / 32f) * 32, (int)Math.Ceiling((e.Y - mapView.SelectionRectangle.Y) / 32f) * 32);



            }

        }

        private void mapView_MouseUp(object sender, MouseEventArgs e)
        {


            if (mapView.SelectionRectangle.Width < 0)
            {

                mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X + mapView.SelectionRectangle.Width,
                                                 mapView.SelectionRectangle.Y,
                                                 mapView.SelectionRectangle.Width,
                                                 mapView.SelectionRectangle.Height);

                mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X,
                                                                 mapView.SelectionRectangle.Y,
                                                                 mapView.SelectionRectangle.Width * -1,
                                                                 mapView.SelectionRectangle.Height);
            }

            if (mapView.SelectionRectangle.Height < 0)
            {

                mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X,
                                                 mapView.SelectionRectangle.Y + mapView.SelectionRectangle.Height,
                                                 mapView.SelectionRectangle.Width,
                                                 mapView.SelectionRectangle.Height);

                mapView.SelectionRectangle = new Rectangle(mapView.SelectionRectangle.X,
                                                                 mapView.SelectionRectangle.Y,
                                                                 mapView.SelectionRectangle.Width,
                                                                 mapView.SelectionRectangle.Height * -1);
            }


        }

        private void mapView_Resize(object sender, EventArgs e)
        {
            var widthVisible = mapView.Width / 32;
            var heightVisible = mapView.Height / 32;

            var mapWidth = Map.Layers[0].Width;
            var mapHeight = Map.Layers[0].Height;

            scrollVertical.Maximum = mapHeight - 0 - heightVisible / 2;
            scrollHorizontal.Maximum = mapWidth - widthVisible / 2;

            if (scrollVertical.Value > scrollVertical.Maximum)
                scrollVertical.Value = scrollVertical.Maximum;

            if (scrollHorizontal.Value > scrollHorizontal.Maximum)
                scrollHorizontal.Value = scrollHorizontal.Maximum;


            if (mapView.screen != null)
                RecalcBars();


        }

        private void scrollVertical_Scroll(object sender, ScrollEventArgs e)
        {
            RecalcBars();
        }

        private void RecalcBars()
        {
            mapView.Camera.Pos = new Vector2(scrollHorizontal.Value * 32, scrollVertical.Value * 32);
        }

        private void scrollHorizontal_Scroll(object sender, ScrollEventArgs e)
        {
            RecalcBars();
        }
    }
}
