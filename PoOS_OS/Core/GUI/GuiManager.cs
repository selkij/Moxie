using System;
using System.Drawing;
using Cosmos.Core;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using ProjectOrizonOS.Application;
using ProjectOrizonOS.Libraries.Graphics;
using Bitmap = ProjectOrizonOS.Libraries.Graphics.Bitmap;
using Canvas = ProjectOrizonOS.Libraries.Graphics.Canvas;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;

namespace ProjectOrizonOS.Core.GUI
{
    public class GuiManager
    {
        public static Canvas canvas;
        
        public static uint screenWidth = 1024;
        public static uint screenHeight = 768;
        
        public static Bitmap wallpaperHD;
        public static Bitmap wallpaper1024x768;
        public static Bitmap cursorImage;
        public static Bitmap logoWhite300x300;
        public static Bitmap logo30x30;
        
        //Buttons
        public static Bitmap closeButton;
        public static Bitmap maximizeButton;
        public static Bitmap minimizeButton;

        public bool pressed;
        
        Desktop desktop;
        Cursor cursor;
        
        WindowManager windowManager = new();

        public App debug = new("Debug", AppType.TextWindow);

        public GuiManager()
        {
            
        }

        public void Initialize()
        {
            LoadFiles();

            canvas = new((int) screenWidth, (int) screenHeight, false);
            
            desktop = new();
            cursor = new();
            
            cursor.Initialize(screenWidth, screenHeight);
            
            AppManager.AddNewApp(debug);
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
            
            //Buttons
            closeButton = new Bitmap(Files.CloseButton);
            maximizeButton = new Bitmap(Files.MaximizeButton);
            minimizeButton = new Bitmap(Files.MinimizeButton);
        }
        
        public void Crash(string error)
        {
            canvas.Clear(Color.Red);
            canvas.DrawBitmap((int) (canvas.Width / 2 - logoWhite300x300.Width / 2), (int) (canvas.Height / 2 - logoWhite300x300.Height / 2 - 89), logoWhite300x300);
            canvas.DrawString(5, 0, $"An error occured and kernel stopped: {error}", Color.White);
            
            Cosmos.System.PCSpeaker.Beep();
            Cosmos.System.PCSpeaker.Beep();
            
            //lock up
            while (true)
            {
                
            }
        }
    }
}