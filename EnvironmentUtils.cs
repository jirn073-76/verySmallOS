using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.ScanMaps;
using System.Threading;
using System.Drawing;

namespace VerySmallOS
{
    static class EnvironmentUtils
    {
        /// <summary>
        /// Asks user if he wants to and gracefully shuts down kernel.
        /// </summary>
        /// <param name="kernel"></param>
        public static void Exit()
        {
            Console.WriteLine("Are you sure? (Y/N)");
            string isSure = Console.ReadLine();
            if (isSure.ToUpper() == "Y")
            {
                Console.WriteLine("Goodbye. :)", Color.Firebrick);
                Thread.Sleep(30);
                Sys.Power.Shutdown();
            }
        }
        /// <summary>
        /// Asks user for choice of Keymap and subsequently returns chosen keymap.
        /// </summary>
        /// <returns></returns>
        public static Sys.ScanMapBase QueryUserForKeymap()
        {
            Console.WriteLine("Would you rather use a US, DE or FR keymap? \n[ I'm sorry we don't yet support more than those :( ]");
            string choice = "";
            Sys.ScanMapBase keymap = null;

            while (keymap is null)
            {
                choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "US":
                        keymap = new US_Standard();
                        break;
                    case "DE":
                        keymap = new DE_Standard();
                        break;
                    case "FR":
                        keymap = new FR_Standard();
                        break;
                    default:
                        Console.WriteLine("Choice not recognized, please choose either of those: {US, DE, FR}");
                        break;
                }
            }

            return keymap;
        }
    }
}
