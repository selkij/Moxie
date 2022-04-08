using System;
using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Interpreter;
using ProjectOrizonOS.Properties;
using Sys = Cosmos.System;

namespace ProjectOrizonOS.Core
{
    public class Setup
    {
        public string name { get; set; }
        
        private skpParse skpParser = new();
        
        public void StartSetup()
        {
            if (VFSManager.DirectoryExists(@"0:\SYSTEM\"))
            {
                try
                {
                    ShellInfo.firstRunning = false;
                    skpParser.Execute(file:"cfg.skp");
                } catch(Exception ex)
                {
                    Kernel.shell.WriteLine(ex.ToString(), type: 4);
                    Console.ReadKey();
                    Sys.Power.Shutdown();
                }
            }
            else
            {
                Kernel.shell.Log("Creating SYSTEM folder", 1);
                
                try
                {
                    VFSManager.CreateDirectory(@"0:\SYSTEM");
                    Kernel.shell.Log("Created SYSTEM folder", 2);
                }
                catch (Exception ex)
                {
                    Kernel.shell.Log($"Failed Creating SYSTEM folder: {ex}", 3);
                }
                
                Kernel.shell.Log("Creating users.skp", 1);
                try
                {
                    VFSManager.CreateFile(@"0:\SYSTEM\users.skp");
                    Kernel.shell.Log("Created users.skp file", 2);
                }
                catch (Exception ex)
                {
                    Kernel.shell.Log($"Failed Creating users.skp file: {ex}", 3);
                }
                
                while (true)
                {
                    Kernel.shell.WriteLine("What is your name?", ConsoleColor.Gray);
                    name = Console.ReadLine();
                
                    Kernel.shell.WriteLine($"Are you sure? Is {name} correct? [Y/N O/N]");
                    string cName = Console.ReadLine();
                
                    if (cName.StartsWith("y"))
                    {
                        //Adding user to users.skp
                        Kernel.shell.Log("Creating users.skp and adding user...", 1);
                        try
                        {
                            VFSManager.CreateFile(@"0:\SYSTEM\users.skp");
                            
                            //For now it is broken and useless
                            /*User.User user = new User.User();
                            
                            user.AddUser(name);*/
                
                            ShellInfo.user = name;
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
}