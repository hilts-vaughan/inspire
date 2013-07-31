using System;
using System.Linq;
using System.Threading;
using GameServer.Models;
using GameServer.Network;
using GameServer.Services.Auth;
using Inspire.Shared.Service;

namespace GameServer
{
    class Lobby
    {


        static AuthenticationService _authenticationService = new AuthenticationService();

        static ServiceContainer   _serviceContainer = new ServiceContainer();

        static void Main(string[] args)
        {

            // Setup some stuff, because why not
            Console.Title = "Blasters Lobby Gateway";
            Console.WindowWidth = 100;

            // Write some welcoming info
            PrintLine(ConsoleColor.Yellow, "********************************************************************");
            PrintLine(ConsoleColor.Yellow, "This is beta software. You have been warned. Code by: Vaughan Hilts");
            PrintLine(ConsoleColor.Yellow, "********************************************************************\n");

            //Logger.Instance.Log(Level.Info, "The lobby server has succesfully been started.");


            // Add services
            _serviceContainer.RegisterService(_authenticationService);


            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ClientNetworkManager.Instance.Update();

            // We populate the game with some matches in debug mode


#if DEUBG
      
#endif

//            Thread.Sleep(5000);

            // Query for an item
            var context = new ServerContext();
            var item = context.ItemTemplates.FirstOrDefault();




            var done = false;




            while (true)
            {


                ClientNetworkManager.Instance.Update();

                _serviceContainer.PerformUpdates();



                Thread.Sleep(1);


#if DEBUG_MOCK

                if (done || _appServerService.ApplicationServers.Count == 0)
                    continue;

                // Dispatch a dummy match to the server in debug mode
                var _client = new NetClient(new NetPeerConfiguration("Inspire"));
               _client.Start();
                _client.Connect("localhost", 8787);
                Thread.Sleep(2000);
                var user = new BlastersShared.Models.User(_client.ServerConnection, "Vaughan");
                _authenticationService.AddUser(user);

                var demoSession = _gameSessionService.CreateSession();
                demoSession.Configuration.MaxPlayers = 1;


                _gameSessionService.AddToSession(user, demoSession);
                _gameSessionService.ActivateSession(demoSession);
                done = true;
#endif


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
