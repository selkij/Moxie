using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;

using SkippleOS.Shell;

namespace SkippleOS
{
    public class Kernel : Sys.Kernel
    {

        public string name { get; set; }

        public string input { get; set; }
        public static string[] args = new string[] { };

        private Sys.FileSystem.CosmosVFS fs;
        private string current_directory = @"0:\";

        private ShellManager shell = new ShellManager();

        // Check if it contains volume
        public static bool ContainsVolumes()
        {
            if (VFSManager.GetVolumes().Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void BeforeRun()
        {
            // Initiating file system
            try
            {
                shell.WriteLine("Initiating file system...", type: 1);
                fs = new Sys.FileSystem.CosmosVFS();
                VFSManager.RegisterVFS(fs);
            } catch(Exception ex)
            {
                if(ex is IndexOutOfRangeException)
                {
                    shell.Write(ex.ToString(), type: 3);
                }
                shell.Write(ex.ToString(), type: 3);
                Console.ReadKey();
                Stop();
            } 
            shell.WriteLine("File system initiated", type: 2);

            #region Booted Section
            ShellInfo.isRunning = true;

            shell.WriteLine($"SkippleOS {ShellInfo.version} booted.", type: 2);
            shell.WriteLine("Press a key to continue", ConsoleColor.Black, ConsoleColor.White);

            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Login section
            if (ShellInfo.firstRunning)
            {
                shell.WriteLine("First time starting SkippleOS. Creating user...", type: 1);
                shell.WriteLine("What is your name?", ConsoleColor.Gray, ConsoleColor.Black);
                name = Console.ReadLine();

                shell.WriteLine($"Are you sure? Is {name} correct? [Y/N O/N]");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "yes":
                    case "y":
                    case "oui":
                    case "o":
                        ShellInfo.user = name;
                        ShellInfo.firstRunning = false;
                        shell.WriteLine("User created!", type: 2);
                        break;
                    case "no":
                    case "n":
                    case "non":
                        Console.Clear();
                        shell.WriteLine("What is your name?", ConsoleColor.Gray, ConsoleColor.Black);
                        name = Console.ReadLine();
                        break;
                    default:
                        ShellInfo.user = name;
                        ShellInfo.firstRunning = false;
                        break;
                }
            }
            #endregion

            Console.Clear();
            
        }

        protected override void Run()
        {
            try
            {
                Start(name);

                input = Console.ReadLine();
                shell.ExecuteCommand(input.Split(' '));
            } catch (Exception ex)
            {
                Console.Clear();
                shell.WriteLine(ex.ToString(), type: 3);
                Console.ReadKey();
                Stop();
            }

            
        }
        
        public void Start(string _name)
        {
            Console.ForegroundColor = ConsoleColor.White;

            shell.Write("\n" + _name, ConsoleColor.Cyan, ConsoleColor.Black);
            shell.Write("@", ConsoleColor.Gray, ConsoleColor.Black);
            shell.Write("SkippleOS", ConsoleColor.Green, ConsoleColor.Black);
            shell.Write("<", ConsoleColor.Gray, ConsoleColor.Black);
            shell.Write(current_directory, ConsoleColor.Yellow, ConsoleColor.Black);
            shell.Write(">", ConsoleColor.Gray, ConsoleColor.Black);
        }

        

    }
}
