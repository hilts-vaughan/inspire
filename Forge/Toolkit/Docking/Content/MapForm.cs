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
using Microsoft.Xna.Framework.Input;
using Toolkit.Mapping;
using ButtonState = System.Windows.Forms.ButtonState;

namespace Toolkit.Docking.Content
{
    public partial class MapForm : ToolWindow, ISaveable
    {

        /// <summary>
        /// The current layer
        /// </summary>
        public int CurrentLayer { get; set; }

        public MapForm()
        {
            InitializeComponent();

            Application.Idle += Application_Idle;

            // Create a backup stack that can go thirty items into the past
            BackupStack = new LimitedStack<GameMapSnapshot>(30);

        }

        private DateTime then = DateTime.Now;

        void Application_Idle(object sender, EventArgs e)
        {
            if ((DateTime.Now - then).Milliseconds > 500)
            {
                then = DateTime.Now;
                Update();
            }
        }

        private MapTemplate _template;

        public GameMap Map { get; set; }

        public LimitedStack<GameMapSnapshot> BackupStack { get; set; }

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

            var snapshot = (GameMap) SerializationHelper.ByteArrayToObject(SerializationHelper.ObjectToByteArray(Map));
            BackupStack.Push(new GameMapSnapshot(snapshot,  MapEditorGlobals.ActiveActionType));

            while (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {

                mouseState = Mouse.GetState();

                var x = mouseState.X / 32;
                var y = mouseState.Y / 32;

                if (prevX == x && prevY == y)
                    continue;

                if (x < 0 || y < 0)
                    continue;

                prevX = x;
                prevY = y;


                var currentTool = MapEditorGlobals.ActiveActionType;
                var action = (IMapAction)Activator.CreateInstance(currentTool);
                action.Execute(Map, x, y, CurrentLayer);


                // Refresh and paginate

                Refresh();
                Application.DoEvents();

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
    }
}
