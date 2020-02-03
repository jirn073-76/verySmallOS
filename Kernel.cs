using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.ScanMaps;
using Cosmos.Debug.Kernel;
using Cosmos.HAL.BlockDevice;

//128, 38
namespace VerySmallOS
{
    public class Kernel : Sys.Kernel
    {
        //public static Sys.FileSystem.FileSystem fileSystem;
        public static Sys.FileSystem.CosmosVFS fileSystem;
        string[] supportedCommands = new string[] { "ls", "dir", "mkdir", "cat", "exit", "startx", "help", "touch"};
        string currentPath = "0:";
        string[] cmd;
        public static Debugger debugger;

        public void RunCommand(string[] cmd)
        {
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
                case "touch":
                    FileSystemUtils.Touch(cmd[1]);
                    break;
                case "mkdir":
                    FileSystemUtils.Mkdir(cmd[1]);
                    break;
                case "help":
                    for (int i = 0; i < supportedCommands.Length; i++)
                        Console.Write($"{supportedCommands[i]}\t");
                    Console.Write('\n');
                    break;
                case "exit":
                    EnvironmentUtils.Exit();
                    break;
                case "startx":
                    Console.WriteLine("Are you sure you want to change to graphical mode? (Y/N)");
                    string isSure = Console.ReadLine();
                    if (isSure.ToUpper() != "Y")
                        return;
                    Console.WriteLine("For cursor mode press 1, for shape mode press 2");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            GraphicsSubsystemUtil.StartGraphicalMode(System.Drawing.Color.Wheat);
                            GraphicsSubsystemUtil.EnableCursor();
                            break;
                        case "2":
                            GraphicsSubsystemUtil.StartGraphicalMode(System.Drawing.Color.Wheat);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. :/\nBack to console mode. ");
                            break;
                    }
                    break;
                default:
                    if (cmd[0] != "")
                        Console.WriteLine("Command not recognized");
                    break;
            }
        }

        protected override void BeforeRun()
        {
            debugger = mDebugger;
            Console.Clear();
            Console.WriteLine("Welcome to verySmallOS\n");
            SetKeyboardScanMap(EnvironmentUtils.QueryUserForKeymap());

            /*
            var fsFactory = new Sys.FileSystem.FatFileSystemFactory();
            var blockDevice = BlockDevice.Devices.First();
            var partition = new Partition(blockDevice, 254, 50000);
            fileSystem = fsFactory.Create(partition, "0:", 47000);
            */

            fileSystem = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fileSystem);
            Console.WriteLine("\n(Use \"startx\" to enter the rudimentary graphical mode :))\n\n\n\n\n\n");
        }

        protected override void Run()
        {
            try
            {
                if (GraphicsSubsystemUtil.IsGraphicsSubsystemRunning)
                {
                    GraphicsSubsystemUtil.Redraw();
                }
                else
                {
                    cmd = Console.ReadLine().Split(' ');
                    RunCommand(cmd);
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
