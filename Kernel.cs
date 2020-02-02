using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.IO;
using System.Drawing;
using Cosmos.System.ScanMaps;

//128, 38
namespace VerySmallOS
{
    public class Kernel : Sys.Kernel
    {
        Canvas canvas;
        Sys.FileSystem.CosmosVFS fileSystem;

        string[] supportedCommands = new string[] { "ls, dir, mkdir, cat, exit, startx, help"};
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Welcome to verySmallOS\n", Color.Red);
            SetKeyboardScanMap(EnvironmentUtils.QueryUserForKeymap());

            fileSystem = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fileSystem);
            Console.WriteLine("\n\n\n\n\n\n\n");

            //canvas = FullScreenCanvas.GetFullScreenCanvas();
            //canvas.Clear(Color.BlanchedAlmond);
        }

        protected override void Run()
        {
            try
            {
                string currentPath = "0:";
                string[] cmd = Console.ReadLine().Split(' ');
                FileSystemUtils.CheckForFileinsertOperator(cmd, out bool containsWriteOp);

                if (containsWriteOp)
                    return;
                    
                switch (cmd[0])
                {
                    case "ls":
                    case "dir":
                        FileSystemUtils.Dir(currentPath, cmd);
                        break;
                    case "cat":
                        FileSystemUtils.Cat(cmd[1]);
                        break;
                    case "mkdir":
                        FileSystemUtils.Mkdir(cmd[1]);
                        break;
                    case "help":
                        for (int i = 0; i < supportedCommands.Length; i++)
                            Console.Write($"{supportedCommands[i]}\t");
                        break;
                    case "exit":
                        EnvironmentUtils.Exit();
                        break;
                    default:
                        Console.WriteLine("Command not recognized");
                        break;
                }
            }
            catch (Exception e)
            {
                mDebugger.Send("Exception occurred: " + e.Message);
                mDebugger.Send(e.Message);
                Stop();
            }
        }
    }
}
