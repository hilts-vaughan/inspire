#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Inspire.GameEngine.ScreenManager
{
    /// <summary>
    /// The main class.
    /// </summary>
    

    // WARNING: HERE BE PANDAS

    /*
     * 
     *           .--.
         / _  \  ___      .--.
        | ( _.-""   `'-.,' _  \
         \.'            '.  ) /
         /                \_.'
        /    .-.   .-.     \
        |   / o \ / o \    |
        ;   \.-'` `'-./    |
        /\      ._.       /
      ;-'';_   ,_Y_,   _.'
     /     \`--.___.--;.
    /|      '.__.---.  \\
   ;  \              \  ;'--. .-.
   |   '.    __..-._.'  |-,.-'  /
   |     `""`  .---._  / .--.  /
  / ;         /      `-;/  /|_/
  \_,\       |            | |
  jgs '-...--'._     _..__\/
     * 
     * 
     * 
     */


    public static class Program
    {
        private static PuzzleGame game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            game = new PuzzleGame();
            game.Run();
        }
    }
}
