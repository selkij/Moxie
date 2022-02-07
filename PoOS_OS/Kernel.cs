using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Shell;
using System.Text;
using Cosmos.System.Network.Config;
using ProjectOrizonOS.Shell.Network;
using ProjectOrizonOS.Shell.Cmds;

namespace ProjectOrizonOS
{
    public class Kernel : Sys.Kernel
    {
        public string name { get; set; }
        public string input { get; set; }

        private Sys.FileSystem.CosmosVFS fs;
        public static string current_directory = @"0:\";

        private ShellManager shell = new();

        private CommandManager cManager = new();

        private NetworkManager networkManager = new();

        protected override void BeforeRun()
        {
            // Initiating file system
            try
            {
                shell.WriteLine("Initiating file system...", type: 1);
                fs = new Sys.FileSystem.CosmosVFS();
                VFSManager.RegisterVFS(fs);
            }
            catch (Exception ex)
            {
                shell.Write(ex.ToString(), type: 4);
                Console.ReadKey();
                Stop();
            }
            shell.WriteLine("File system initiated", type: 2);


            // Check if the network connection correctly ethablished via DHCP
            shell.WriteLine("Initiating Network connection via DHCP...", type: 1);
            bool skip = true;
            try
            {
                if(skip == false)
                {
                    networkManager.DCHPConnect();
                } else
                {
                    shell.WriteLine("Process Skipped!", type: 1);
                }  
                
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }

            #region Booted Section
            shell.WriteLine($"ProjectOrizonOS {ShellInfo.version} DEV channel booted.", type: 2);
            shell.WriteLine("Press a key to continue", ConsoleColor.Black, ConsoleColor.White);

            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Login section
            if (ShellInfo.firstRunning)
            {
                shell.WriteLine("First time starting ProjectOrizonOS. Creating user...", type: 1);
                shell.WriteLine("What is your name?", ConsoleColor.Gray, ConsoleColor.Black);
                name = Console.ReadLine();

                shell.WriteLine($"Are you sure? Is {name} correct? [Y/N O/N]");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "yes":
                    case "y":
                    case "oui":
                    case "o":
                        bool exception = false;

                        try
                        {
                            VFSManager.CreateFile(@"0:\cfg.skp");

                            var cfg_file = VFSManager.GetFile(@"0:\cfg.skp");
                            var cfg_file_stream = cfg_file.GetFileStream();

                            if (cfg_file_stream.CanWrite)
                            {
                                byte[] cfg_output = Encoding.ASCII.GetBytes($"name: {name}\nkeyMap: {ShellInfo.langSelected}");
                                cfg_file_stream.Write(cfg_output, 0, cfg_output.Length);
                            }

                            exception = true;
                        }
                        catch (Exception ex)
                        {
                            shell.WriteLine(ex.ToString(), type: 3);
                        }

                        if (exception)
                        {
                            ShellInfo.user = name;
                            ShellInfo.firstRunning = false;
                        }

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
                cManager.ExecuteCommand(input.Split(' '));
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }

        public void Start(string _name)
        {
            Console.ForegroundColor = ConsoleColor.White;

            shell.Write("\n" + _name, ConsoleColor.Cyan, ConsoleColor.Black);
            shell.Write("@", ConsoleColor.Gray, ConsoleColor.Black);
            shell.Write("POrizonOS", ConsoleColor.Green, ConsoleColor.Black);
            shell.Write("<", ConsoleColor.Gray, ConsoleColor.Black);
            shell.Write(current_directory, ConsoleColor.Yellow, ConsoleColor.Black);
            shell.Write(">", ConsoleColor.Gray, ConsoleColor.Black);
        }
    }
}
