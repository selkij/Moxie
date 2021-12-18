using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;


namespace Skipple
{
    public class Kernel : Sys.Kernel
    {

        public string name { get; set; }
        public string input { get; set; }

        private Sys.FileSystem.CosmosVFS fs;
        private string current_directory = "@0:/";

        //private string fs_type = VFSManager.GetFileSystemType("@0:/");
        //private long available_space = VFSManager.GetAvailableFreeSpace("@0:/");

        protected override void BeforeRun()
        {
            // Initiating file system
            fs = new Sys.FileSystem.CosmosVFS();
            VFSManager.RegisterVFS(fs);

            Console.Clear();
            Console.Beep();
            Console.WriteLine("SkippleOS 1.0 booted successfully.");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Please press a key to continue...");
            Console.ReadKey();
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("What is your name?");
            Console.ForegroundColor = ConsoleColor.White;
            name = Console.ReadLine();
            Console.Clear();
        }


        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n" + name);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("@");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("SkippleOS");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(">");
            Console.ForegroundColor = ConsoleColor.White;

            input = Console.ReadLine();
            string[] inputArray = input.Split(' ', '\t');

            foreach (var array in inputArray)
            {
                Console.WriteLine(array);
            }


            switch (input)
            {
                case "clear":
                    Console.Clear();
                    break;

                case "cls":
                    Console.Clear();
                    break;

                case "about":
                    Console.Write("SkippleOS ");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("1.0\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Made with <3 with Cosmos in C#");
                    break;

                case "whoami":
                    Console.WriteLine(name);
                    break;

                /*case "beep" + inputArray[1]:
                    int stop;
                    for (stop = 0; stop = numberBeep; stop++ )
                    { 
                        Console.Beep();
                    }

                    break:*/

                // ==== File ====
                
                /* "fileman":
                    Console.WriteLine("Available Free Space: " + available_space);
                    Console.WriteLine("File system: " + fs_type);
                    break;*/

                case "rmdir":
                    string folder_select = Console.ReadLine();


                    if (VFSManager.DirectoryExists(folder_select))
                    {
                        VFSManager.DeleteDirectory(folder_select, false);
                    }
                    else
                    {
                        Console.WriteLine("Ce fichier n'éxiste pas");
                    }
                    
                    break;

                case "mkdir":
                    string folder_selectt = Console.ReadLine();

                    try {
                        VFSManager.CreateDirectory("@0:/" + folder_selectt);
                    } catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    break;

                case "ls":
                    var directory_list = VFSManager.GetDirectoryListing("@0:/");
                    foreach (var directoryEntry in directory_list)
                    {
                        Console.WriteLine(directoryEntry.mName);
                    }

                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("ERROR:");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" " + input);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" command unknown");
                    break;

            }
        }
    }
}
