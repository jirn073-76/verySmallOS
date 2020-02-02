using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace VerySmallOS
{
    static class FileSystemUtils
    {
        /// <summary>
        /// Creates directory if it doesn't exist yet. Takes only absolute paths.
        /// </summary>
        /// <param name="path"></param>
        public static void Mkdir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Directory " + path + " created");
            }
            else
                Console.WriteLine("Directory " + path + " already exists");
        }

        /// <summary>
        /// Prints contents of specified file to stdout if file exists. Takes only absolute paths.
        /// </summary>
        /// <param name="path"></param>
        public static void Cat(string path)
        {
            if (File.Exists(path))
            {
                int lc = 0;
                StreamReader fstream = new StreamReader(path);
                while (!fstream.EndOfStream)
                {
                    lc++;
                    Console.WriteLine(fstream.ReadLine());
                }

                Console.WriteLine($"{path} finished outputting ({lc} line{(lc == 1 ? "" : "s")} in total)");
            }

            else
                Console.WriteLine("File " + path + " not found.");
        }

        /// <summary>
        /// Prints content of specified directory. If no directory is specified, defaulting to specified defaultPath.
        /// </summary>
        /// <param name="defaultPath"></param>
        /// <param name="cmd"></param>
        public static void Dir(string defaultPath, string[] cmd)
        {
            string[] files, dirs;
            if (cmd.Length == 1)
            {
                files = Directory.GetFiles(defaultPath);
                dirs = Directory.GetDirectories(defaultPath);
            }
            else
            {
                files = Directory.GetFiles(cmd[1]);
                dirs = Directory.GetDirectories(cmd[1]);
            }

            int i;
            for (i = 0; i < files.Length; i++)
                Console.WriteLine(i + ": " + files[i]);

            for (int j = 0; j < dirs.Length; j++)
                Console.WriteLine(++i + ": " + dirs[j]);
        }

        /// <summary>
        /// Checks if >> operator was used and inserts a line break + specified text into specified file.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="containsWriteOp">Reports if write operator was found</param>
        public static void CheckForFileinsertOperator(string[] cmd, out bool containsWriteOp)
        {
            containsWriteOp = false;

            for (int i = 0; i < cmd.Length; i++)
                if (cmd[i] == ">>")
                    containsWriteOp = true;

            if (containsWriteOp)
            {
                int indOfOp = Array.IndexOf(cmd, ">>");
                var file = cmd[indOfOp + 1];
                if (File.Exists(file))
                {
                    StreamWriter fstream = new StreamWriter(file, true);
                    string inp = "";
                    for (int i = 0; i < indOfOp; i++)
                        inp += cmd[i] + " ";

                    fstream.WriteLine(inp);
                    fstream.Flush();
                }
            }
        }
    }
}
