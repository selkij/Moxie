using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Shell;
using System.Text;
using ProjectOrizonOS.Shell.Network;
using ProjectOrizonOS.Shell.Cmds;
using ProjectOrizonOS.Interpreter;

namespace ProjectOrizonOS
{
    public class Kernel : Sys.Kernel
    {
        //User Properties
        public string name { get; set; }
        public string input { get; set; }

        //vFS
        public static string current_directory = @"0:\";
        public static string current_volume = @"0:\";

        //Instantiate
        private ShellManager shell = new();
        private Initializer init = new();
        private CommandManager cManager = new();
        private skpParse skpParser = new();

        protected override void BeforeRun()
        {
            init.vFS();
            init.DHCP();

            #region Booted Section
            shell.Log($"ProjectOrizonOS {ShellInfo.version} DEV channel booted.", type: 2);
            shell.WriteLine("\n Press a key to continue", ConsoleColor.Black, ConsoleColor.White);

            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Login section
            if (VFSManager.FileExists(@"0:\cfg.skp"))
            {
                try
                {
                    ShellInfo.firstRunning = false;
                    skpParser.Execute(file:"cfg.skp");
                } catch(Exception ex)
                {
                    shell.WriteLine(ex.ToString(), type: 4);
                    Console.ReadKey();
                    Sys.Power.Shutdown();
                }
            }

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

                        shell.WriteLine("Machine name?", type: 1);
                        string MName = Console.ReadLine();
                        ShellInfo.machineName = MName;

                        try
                        {
                            VFSManager.CreateFile(@"0:\cfg.skp");

                            var cfg_file = VFSManager.GetFile(@"0:\cfg.skp");
                            var cfg_file_stream = cfg_file.GetFileStream();

                            if (cfg_file_stream.CanWrite)
                            {
                                byte[] cfg_output = Encoding.ASCII.GetBytes($"name={name}\nkeyMap={ShellInfo.langSelected}\nmachineName={ShellInfo.machineName}");
                                cfg_file_stream.Write(cfg_output, 0, cfg_output.Length);
                            }

                            ShellInfo.user = name;
                        }
                        catch (Exception ex)
                        {
                            shell.WriteLine(ex.ToString(), type: 3);
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
                        shell.WriteLine("User created!", type: 2);
                        break;
                }
                
                ShellInfo.firstRunning = false;
            }
            #endregion

            Console.Clear();
        }

        protected override void Run()
        {
            try
            {
                Start(ShellInfo.user);

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

            shell.Write("\n" + _name, ConsoleColor.DarkGreen, ConsoleColor.Black);
            shell.Write("@", ConsoleColor.Gray, ConsoleColor.Black);
            shell.Write(ShellInfo.machineName, ConsoleColor.Yellow, ConsoleColor.Black);
            shell.Write("<", ConsoleColor.Gray, ConsoleColor.Black);
            shell.Write(current_directory, ConsoleColor.DarkBlue, ConsoleColor.Black);
            shell.Write("> #", ConsoleColor.Gray, ConsoleColor.Black);
        }
    }
}
