using System;
using System.Text;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Cosmos.HAL;
using ProjectOrizonOS.Shell;
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
        private Sys.FileSystem.CosmosVFS fs;

        public static string current_directory = @"0:\";
        public static string current_volume = @"0:\";

        //Instantiate
        private ShellManager shell = new();
        private Initializer init = new();
        private CommandManager cManager = new();
        private skpParse skpParser = new();

        //Graphics
        public static Canvas canvas;
        public static Bitmap wallpaperHD;
        public static Bitmap wallpaper1024_768;
        public static Bitmap cursor;

        //Pens
        Pen topPen = new(Color.White);
        Pen textPen = new(Color.Black);
        Pen gray = new(Color.Gray);

        //Screen Res
        public static uint screenWidth = 1920;
        public static uint screenHeight = 1080;

        //Mouse state
        public static bool pressed;

        protected override void BeforeRun()
        {
            init.vFS();
            init.DHCP();

            #region Booted Section
            shell.Log($"ProjectOrizonOS {ShellInfo.version} DEV channel booted.", type: 2);
            shell.WriteLine("\n Press a key to continue", ConsoleColor.Black, ConsoleColor.White);
            #endregion

            #region Login section
            if (ShellInfo.firstRunning && ShellInfo.shellMode == "textVGA") {
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
              } else {
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

            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;

            Console.Clear();

            if(ShellInfo.shellMode == "canvas")
            {
                LoadFiles();
                canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode((int)screenWidth, (int)screenHeight, ColorDepth.ColorDepth32));
            }
        }

        

        protected override void Run()
        {
            if(ShellInfo.shellMode == "textVGA")
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
            } else if(ShellInfo.shellMode == "canvas")
            {
                try
                {
                    canvas.Clear(Color.Black);

                    if(screenWidth == 1920 && screenHeight == 1080)
                    {
                        canvas.DrawImage(wallpaperHD, 0, 0);
                    } else if(screenWidth == 1024 && screenHeight == 768)
                    {
                        canvas.DrawImage(wallpaper1024_768, 0, 0);
                    }
                    canvas.DrawFilledRectangle(topPen, 0, 0, (int)screenWidth, 20);
                    canvas.DrawFilledRectangle(gray, (int)screenWidth / 2 - 30, (int)screenHeight - 40, 100, 40);

                    canvas.DrawString($"{RTC.Hour}:{RTC.Minute}.{RTC.Second}", PCScreenFont.Default, textPen, new Sys.Graphics.Point((int) screenWidth - 70, 3));

                    DrawCursor(Sys.MouseManager.X, Sys.MouseManager.Y);

                    canvas.Display();
                } catch (Exception ex)
                {
                    mDebugger.Send(ex.ToString());
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
            wallpaper1024_768 = new Bitmap(Files.Wallpaper1024_768);
            cursor = new Bitmap(Files.Cursor);
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
