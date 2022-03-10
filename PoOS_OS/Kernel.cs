using System;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using ProjectOrizonOS.Interpreter;
using ProjectOrizonOS.Shell;
using ProjectOrizonOS.Shell.Cmds;
using Canvas = ProjectOrizonOS.Libraries.Graphics.Canvas;

namespace ProjectOrizonOS
{
    public class Kernel : Sys.Kernel
    {
        //User Properties
        public string name { get; set; }
        public string input { get; set; }

        //vFS
        public static string current_directory = @"0:\";

        //Instantiate
        private ShellManager shell = new();
        private Initializer init = new();
        private CommandManager cManager = new();
        private skpParse skpParser = new();

        //Graphics
        public static Canvas canvas;
        public static Bitmap wallpaperHD;
        public static Bitmap wallpaper1024x768;
        public static Bitmap cursor;
        public static Bitmap logoWhite300x300;
        public static Bitmap logo30x30;

        //Screen Res
        public static uint screenWidth = 1024;
        public static uint screenHeight = 768;
        
        protected override void BeforeRun()
        {
            
        }

        protected override void Run()
        {
            if(ShellInfo.shellMode == 0)
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
            } else if(ShellInfo.shellMode == 1)
            {
                if(ShellInfo.shellMode == 1)
            {
                try
                {
                    LoadFiles();
                    
                    //TODO: Faire un dummy project pour tester
                    Canvas canvas = new((int) screenWidth, (int) screenHeight, true);
                    
                    canvas.DrawBitmap( (int) (canvas.Width / 2 - logoWhite300x300.Width / 2), (int) (canvas.Height / 2 - logoWhite300x300.Height / 2 - 89), logoWhite300x300);
                    canvas.DrawString(canvas.Width / 2 - 89, canvas.Height / 2 + 89, "ProjectOrizonOS is loading, please wait...", Color.White);
                    canvas.Update();
                    
                    init.vFS();
                    init.DHCP();
                }
                catch (Exception ex)
                {
                    shell.Log($"Failed Booting ProjectOrizonOS: {ex}", 3);
                }
            }
            else
            {
                init.vFS();
                init.DHCP();
            }
            
            shell.Log($"ProjectOrizonOS {ShellInfo.version} DEV channel booted.", type: 2);
            
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
            else
            {
                /*if (ShellInfo.shellMode == 1)
                    canvas.Disable();*/
                    
                shell.WriteLine("First time starting ProjectOrizonOS. Creating user...", type: 1);
                shell.WriteLine("What is your name?", ConsoleColor.Gray);
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
                        shell.WriteLine("What is your name?", ConsoleColor.Gray);
                        name = Console.ReadLine();
                        break;
                    default:
                        ShellInfo.user = name;
                        shell.WriteLine("User created!", type: 2);
                        break;
                }
                
                if(ShellInfo.shellMode == 1) 
                    Sys.Power.Reboot();
            }

            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;
            
            Console.Clear();
            
            #region Run
            while (true)
                {
                    try
                    {
                        canvas.Clear();

                        canvas.DrawBitmap(0, 0, wallpaper1024x768);
                        canvas.DrawFilledRectangle(0, 0, (int) screenWidth, 20, Color.White);
                        canvas.DrawFilledRectangle(0, (int) screenHeight - 40, (int) screenWidth, 40, Color.White);
                        canvas.DrawString((int) screenWidth - 70, 3, $"{RTC.Hour}:{RTC.Minute}.{RTC.Second}", Color.Black);
                        canvas.DrawString(2, (int) screenHeight - 70, "fps=" + canvas.FPS, Color.White);
                    
                        DrawCursor(Sys.MouseManager.X, Sys.MouseManager.Y);
                    
                        canvas.Update();
                    } catch (Exception ex)
                    {
                        Crash(canvas, ex.ToString());
                    }
                }
            #endregion
            }
        }

        public static void DrawCursor(uint x, uint y)
        {
            canvas.DrawBitmap((int) x, (int) y, cursor);
        }

        public static void LoadFiles()
        {
            wallpaperHD = new Bitmap(Files.WallpaperHD);
            wallpaper1024x768 = new Bitmap(Files.Wallpaper1024_768);
            logoWhite300x300 = new Bitmap(Files.LogoWhite200_200);
            logo30x30 = new Bitmap(Files.Logo30_30);
            cursor = new Bitmap(Files.Cursor);
        }

        public void Start(string _name)
        {
            Console.ForegroundColor = ConsoleColor.White;

            shell.Write("\n" + _name, ConsoleColor.Cyan);
            shell.Write("@", ConsoleColor.Gray);
            shell.Write("POrizonOS", ConsoleColor.Green);
            shell.Write("<", ConsoleColor.Gray);
            shell.Write(current_directory, ConsoleColor.Yellow);
            shell.Write(">", ConsoleColor.Gray);
        }

        public void Crash(Canvas _canvas, String error)
        {
            _canvas.Clear(Color.Red);
            _canvas.DrawBitmap((int) (canvas.Width / 2 - logoWhite300x300.Width / 2), (int) (canvas.Height / 2 - logoWhite300x300.Height / 2 - 89), logoWhite300x300);
            _canvas.DrawString(5, 0, $"An error occured, kernel stopped: {error}", Color.White);
            _canvas.DrawString(5, 5,"Press a button to reboot the system", Color.White);
            
            Cosmos.System.PCSpeaker.Beep();
            Cosmos.System.PCSpeaker.Beep();
            
            Console.ReadKey();
            Sys.Power.Reboot();
        }
    }
}
