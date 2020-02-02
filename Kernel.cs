using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.ScanMaps;

//128, 38
namespace VerySmallOS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fileSystem;

        string[] supportedCommands = new string[] { "ls, dir, mkdir, cat, exit, startx, help"};
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Welcome to verySmallOS\n");
            SetKeyboardScanMap(EnvironmentUtils.QueryUserForKeymap());

            fileSystem = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fileSystem);
            Console.WriteLine("\n\n\n\n\n\n\n");


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
                    case "startx":
                        Console.WriteLine("Are you sure you want to change to graphical mode? (Y/N)");
                        string isSure = Console.ReadLine();
                        if (isSure.ToUpper() != "Y")
                            return;

                        GraphicsSubsystemUtil.StartGraphicalMode();
                        break;
                    default:
                        if (cmd[0] != "")
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
