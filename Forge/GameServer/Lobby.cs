using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BlastersShared;
using GameServer.Game;
using GameServer.Models;
using GameServer.Network;
using GameServer.Services.Auth;
using GameServer.Services.Editor;
using Inspire.Shared.Service;

namespace GameServer
{
    class Lobby
    {


        static AuthenticationService _authenticationService = new AuthenticationService();
        private static EditorService _editorService;


        static ServerServiceContainer _serviceContainer = new ServerServiceContainer();


        static void Main(string[] args)
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            // Setup some stuff, because why not
            Console.Title = "Inspire Server";
            Console.WindowWidth = 100;

            //Logger.Instance.Log(Level.Info, "The lobby server has succesfully been started.");

            _editorService = new EditorService();

            // Add services
            _serviceContainer.RegisterService(_authenticationService);
            _serviceContainer.RegisterService(_editorService);


            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ClientNetworkManager.Instance.Update();


            // Create a map simulator for each map we need
            using (var context = new ServerContext())
                context.MapTemplates.ToList().ForEach(x => _serviceContainer.MapSimulators.Add(new MapSimulator(x)));


            stopwatch.Stop();
            Logger.Instance.Log(Level.Info, "Succesfully started game loop in " + stopwatch.Elapsed.Seconds + "s");

            while (true)
            {

                ClientNetworkManager.Instance.Update();
                _serviceContainer.PerformUpdates();

                // Perform all required updates for each map
                _serviceContainer.MapSimulators.ForEach(simulator => simulator.ServerServiceContainer.PerformUpdates());

                Thread.Sleep(1);

            }



            Console.ReadLine();


        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Logger.Instance.Log(Level.Fatal, e.ExceptionObject.ToString());
        }


        public static void PrintLine(ConsoleColor consoleColor, string message)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
