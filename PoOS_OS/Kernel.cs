using System;
using System.Text;
using System.Drawing;
using Cosmos.Core.Memory;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Cosmos.HAL;
using ProjectOrizonOS.Interpreter;
using ProjectOrizonOS.Shell;
using ProjectOrizonOS.Shell.Network;
using ProjectOrizonOS.Shell.Cmds;

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

        //Network
        private NetworkManager networkManager = new();

        //Graphics
        public static Canvas canvas;
        public static Bitmap wallpaperHD;
        public static Bitmap wallpaper1024x768;
        public static Bitmap cursor;
        public static Bitmap logoWhite300x300;
        public static Bitmap logo30x30;

        //Pens
        Pen WhitePen = new(Color.White);
        Pen BlackPen = new(Color.Black);
        Pen GrayPen = new(Color.Gray);

        //Screen Res
        public static uint screenWidth = 1280;
        public static uint screenHeight = 800;

        //FPS
        private static int frames;
        public static int fps;
        public static int deltaT;
        
        protected override void BeforeRun()
        {
            //LoadFiles();
            
            if(ShellInfo.shellMode == 1)
            {
                try
                {
                    canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode((int) screenWidth, (int) screenHeight, ColorDepth.ColorDepth32)); canvas.Clear(Color.Black);
                    //canvas.DrawImageAlpha(logoWhite300x300, (int) (canvas.Mode.Columns / 2 - logoWhite300x300.Width / 2), (int) (canvas.Mode.Rows / 2 - logoWhite300x300.Height / 2 - 89));
                    canvas.DrawString("ProjectOrizonOS is loading, please wait...", PCScreenFont.Default, WhitePen, canvas.Mode.Columns / 2 - 89, canvas.Mode.Rows / 2 + 89);
                    canvas.Display();
                    
                    init.vFS();
                    init.DHCP();
                }
                catch (Exception ex)
                {
                    shell.Log(ex.ToString(), 3);
                }
            }
            else
            {
                init.vFS();
                init.DHCP();
            }
            
            shell.Log($"ProjectOrizonOS {ShellInfo.version} DEV channel booted.", type: 2);

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
            else
            {
                if(ShellInfo.shellMode == 1) 
                    canvas.Disable();
                    
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
            #endregion

            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;

            Console.Clear();
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
                try
                {
                    if (deltaT != RTC.Second)
                    {
                        fps = frames;
                        frames = 0;
                        deltaT = RTC.Second;
                    }

                    frames++;
                    
                    canvas.Clear();

                    canvas.DrawImage(wallpaperHD, 0, 0);
                    canvas.DrawFilledRectangle(WhitePen, 0, 0, (int) screenWidth, 20);
                    canvas.DrawFilledRectangle(WhitePen, 0, (int) screenHeight - 40, (int) screenWidth, 40);
                    canvas.DrawString($"{RTC.Hour}:{RTC.Minute}.{RTC.Second}", PCScreenFont.Default, BlackPen, (int) screenWidth - 70, 3);
                    canvas.DrawString("fps=" + fps, PCScreenFont.Default, WhitePen, 2, (int) screenHeight - 70);
                    canvas.DrawImageAlpha(logo30x30, 5, (int) screenHeight - 50);
                    
                    DrawCursor(Sys.MouseManager.X, Sys.MouseManager.Y);
                    
                    canvas.Display();
                } catch (Exception ex)
                {
                    Crash(canvas, ex.ToString());
                }
            }
        }

        public static void DrawCursor(uint x, uint y)
        {
            canvas.DrawImageAlpha(cursor, (int) x, (int) y);
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
            _canvas.DrawString($"An error occured, kernel stopped: {error}", PCScreenFont.Default, WhitePen, 0, 0);
        }
    }
}
