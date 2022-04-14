using System;
using System.IO;
using Cosmos.System.FileSystem.VFS;
using Moxie.Properties;
using Console = System.Console;

namespace Moxie.Core.User
{
    public class Setup
    {
        public string name { get; set; }
        
        public void StartSetup()
        {
            
            Kernel.shell.Log("Creating SYSTEM folder", 1);
            
            try
            {
                Directory.CreateDirectory(@"0:\SYSTEM");
                Kernel.shell.Log("Created SYSTEM folder", 2);
            }
            catch (Exception ex)
            {
                Kernel.shell.Log($"Failed Creating SYSTEM folder: {ex}", 3);
            }
            
            while (true)
            {
                Kernel.shell.WriteLine("> What is your name?", ConsoleColor.Gray);
                name = Console.ReadLine();
            
                Kernel.shell.WriteLine($"Are you sure? Is {name} correct? [Y/N O/N]");
                string choice = Console.ReadLine();
            
                if (choice.StartsWith("y")) 
                {
                    //Adding user to users.skp
                    Kernel.shell.Log("Adding user...", 1);
                    try
                    {
                        VFSManager.CreateFile(@"0:\SYSTEM\hostname.hs");
                        File.WriteAllText(@"0:\SYSTEM\hostname.hs", name);
            
                        Info.user = name;
                    }
                    catch (Exception ex)
                    {
                        Kernel.shell.WriteLine(ex.ToString(), type: 3);
                    }
                    
                    break;
                }
            }
        }
    }
}