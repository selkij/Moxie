using System;
using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Interpreter;
using ProjectOrizonOS.Properties;
using ProjectOrizonOS.Shell;
using Sys = Cosmos.System;

namespace ProjectOrizonOS.Core
{
    public class Setup
    {
        public string name { get; set; }
        
        public ShellManager shell = new ();
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
                    shell.WriteLine(ex.ToString(), type: 4);
                    Console.ReadKey();
                    Sys.Power.Shutdown();
                }
            }
            else
            {
                shell.Log("Creating SYSTEM folder", 1);
                
                try
                {
                    VFSManager.CreateDirectory(@"0:\SYSTEM");
                    shell.Log("Created SYSTEM folder", 2);
                }
                catch (Exception ex)
                {
                    shell.Log($"Failed Creating SYSTEM folder: {ex}", 3);
                }
                
                shell.Log("Creating users.skp", 1);
                try
                {
                    VFSManager.CreateFile(@"0:\SYSTEM\users.skp");
                    shell.Log("Created users.skp file", 2);
                }
                catch (Exception ex)
                {
                    shell.Log($"Failed Creating users.skp file: {ex}", 3);
                }
                
                while (true)
                {
                    shell.WriteLine("What is your name?", ConsoleColor.Gray);
                    name = Console.ReadLine();
                
                    shell.WriteLine($"Are you sure? Is {name} correct? [Y/N O/N]");
                    string cName = Console.ReadLine();
                
                    if (cName.StartsWith("y"))
                    {
                        //Adding user to users.skp
                        shell.Log("Creating users.skp and adding user...", 1);
                        try
                        {
                            VFSManager.CreateFile(@"0:\SYSTEM\users.skp");
                
                            /*User.User user = new User.User();
                            
                            user.AddUser(name);*/
                
                            ShellInfo.user = name;
                        }
                        catch (Exception ex)
                        {
                            shell.WriteLine(ex.ToString(), type: 3);
                        }
                        
                        break;
                    }
                }
            }
            
        }
    }
}