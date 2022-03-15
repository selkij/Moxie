using System;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using ProjectOrizonOS.Application;
using ProjectOrizonOS.Libraries.Graphics;
using Canvas = ProjectOrizonOS.Libraries.Graphics.Canvas;

namespace ProjectOrizonOS.Core.GUI
{
    public class GuiManager
    {
        public static Canvas canvas;
        public VBE vbe;
        
        public static uint screenWidth = 1920;
        public static uint screenHeight = 1080;
        
        public static Bitmap wallpaperHD;
        public static Bitmap wallpaper1024x768;
        public static Bitmap cursorImage;
        public static Bitmap logoWhite300x300;
        public static Bitmap logo30x30;

        public bool pressed;
        
        Desktop desktop;
        Cursor cursor;
        
        WindowManager windowManager = new();

        public GuiManager()
        {
            
        }

        public void Initialize()
        {
            LoadFiles();
            
            vbe = new((int) screenWidth, (int) screenHeight);
            canvas = new();
            
            desktop = new();
            cursor = new();
            
            cursor.Initialize(screenWidth, screenHeight);
            
            AppManager.AddNewApp(new App("Debug", AppType.TextWindow));
        }
        
        public void DrawGUI()
        {
            try
            {
                desktop.Draw();
            
                windowManager.DrawWindows();
            
                cursor.DrawCursor();
                
                switch (Sys.MouseManager.MouseState)
                {
                    case Sys.MouseState.Left:
                        pressed = true;
                        break;
                    case Sys.MouseState.None:
                        pressed = false;
                        break;
                }

                canvas.Update();     
            } catch (Exception ex)
            {
                Crash(ex.ToString());
            }
        }

        public static void LoadFiles()
        {
            wallpaperHD = new Bitmap(Files.WallpaperHD);
            wallpaper1024x768 = new Bitmap(Files.Wallpaper1024_768);
            logoWhite300x300 = new Bitmap(Files.LogoWhite200_200);
            logo30x30 = new Bitmap(Files.Logo30_30);
            cursorImage = new Bitmap(Files.Cursor);
        }
        
        public void Crash(string error)
        {
            Canvas.Clear(Color.Red);
            canvas.DrawBitmap((int) (Canvas.Width / 2 - logoWhite300x300.Width / 2), (int) (Canvas.Height / 2 - logoWhite300x300.Height / 2 - 89), logoWhite300x300);
            Canvas.DrawString(5, 0, $"An error occured and kernel stopped: {error}", Color.White);
            
            Cosmos.System.PCSpeaker.Beep();
            Cosmos.System.PCSpeaker.Beep();
            
            //lock up
            while (true)
            {
                
            }
        }
    }
}