#region File Description
//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Awesomium.Core;
using AwesomiumUiLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace Inspire.GameEngine.ScreenManager
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager
    {
        #region Fields

        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();

        InputState input = new InputState();

        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;
        private Texture2D _cursor;

        bool isInitialized;

        bool traceEnabled;

        #endregion

        #region Properties


        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }




        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }

        public ContentManager ContentManager { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }


        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }


        #endregion

        #region Initialization


        public GameWindow Window { get; set; }

        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(ContentManager contentManager, GraphicsDevice graphicsDevice, GameWindow window)
        {
            // we must set EnabledGestures before we can query for them, but
            // we don't assume the game wants to read them.
            TouchPanel.EnabledGestures = GestureType.None;

            ContentManager = contentManager;
            GraphicsDevice = graphicsDevice;
            Window = window;

            spriteBatch = new SpriteBatch(GraphicsDevice);




            LoadContent();

        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public void Initialize()
        {

            isInitialized = true;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected void LoadContent()
        {
            // Get the cursor location
            _cursor = TextureLoader.GetTexture("cursor.png", GraphicsDevice);
        }




        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Read the keyboard and gamepad.
            input.Update();

            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = false;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }


        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }

            var x = input.MousePosition.X;
            var y = input.MousePosition.Y;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(_cursor, new Vector2(x, y), Color.White);
            spriteBatch.End();

        }

        private AwesomiumUI _ui = new AwesomiumUI();

        #endregion

        #region Public Methods

        private bool _done = false;

        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;
            screen.UiManager = _ui;

            if (!_done)
            {
                var executionPath =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

                var width = GraphicsDevice.PresentationParameters.BackBufferWidth;
                var height = GraphicsDevice.PresentationParameters.BackBufferHeight;

                screen.UiManager.Initialize(GraphicsDevice, width, height, executionPath);


                _ui.webView.ConsoleMessage += WebViewOnConsoleMessage;

                //JSObject jsConsole = UiManager.webView.CreateGlobalJavascriptObject("console");
                //jsConsole.Bind("log", false, JSConsoleLog);
                //jsConsole.Bind("dir", false, JSConsoleLog);

                InputSystem.Initialize(Window);
                InputSystem.CharEntered += CharEnteredHandler;
                InputSystem.KeyUp += KeyUpHandler;
                InputSystem.FullKeyHandler += FullKeyHandler;
                InputSystem.MouseMove += MouseMoveHandler;
                InputSystem.MouseDown += MouseDownHandler;
                InputSystem.MouseUp += MouseUpHandler;



                _done = true;
            }


            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.Initialize();
                screen.LoadContent();
            }


         
            screens.Add(screen);

            // update the TouchPanel to respond to gestures this screen is interested in
            TouchPanel.EnabledGestures = screen.EnabledGestures;
        }


        private void JSConsoleLog(object sender, JavascriptMethodEventArgs e)
        {
            Console.WriteLine(e.Arguments[0].ToString());
        }

        private void WebViewOnConsoleMessage(object sender, ConsoleMessageEventArgs consoleMessageEventArgs)
        {
            Console.WriteLine(consoleMessageEventArgs.Message);
        }


        public void FullKeyHandler(object sender, uint msg, IntPtr wParam, IntPtr lParam)
        {
            _ui.InjectKeyboardEvent((int)msg, (int)wParam, (int)lParam);
        }

        public void KeyUpHandler(object sender, KeyEventArgs e)
        {


        }




        public void CharEnteredHandler(object sender, CharacterEventArgs e)
        {
        }


        public void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            _ui.InjectMouseMove(e.Location.X, e.Location.Y);
        }

        public void MouseDownHandler(object sender, MouseEventArgs e)
        {
            _ui.InjectMouseDown(e.Button);
        }

        public void MouseUpHandler(object sender, MouseEventArgs e)
        {
            _ui.InjectMouseUp(e.Button);
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
            if (screens.Count > 0)
            {
                TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);

            spriteBatch.End();
        }


        #endregion
    }
}
