using System;
using Sys = Cosmos.System;
using ProjectOrizonOS.Application;
using Canvas = ProjectOrizonOS.Libraries.Graphics.Canvas;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;
using Files = ProjectOrizonOS.Resources.Files;

namespace ProjectOrizonOS.Core.GUI
{
    public class GuiManager
    {
        public static Canvas canvas;
        
        public static uint screenWidth = 1024;
        public static uint screenHeight = 768;
        
        public bool pressed;
        
        Desktop desktop;
        
        WindowManager windowManager = new();

        public App debug = new("Debug", AppType.TextWindow);
        //public App pong = new("Pong", AppType.GraphicsWindow);

        public void Initialize()
        {
            
            desktop = new();
            
            AppManager.AddNewApp(debug);
            //AppManager.AddNewApp(pong);
        }
        
        public void DrawGUI()
        {
            try
            {
                desktop.Draw();
            
                windowManager.DrawWindows();
            
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

        public void Crash(string error)
        {
            canvas.Clear(Color.Red);
            canvas.DrawBitmap((int) (canvas.Width / 2 - Files.LogoWhite300X300.Width / 2), (int) (canvas.Height / 2 - Files.LogoWhite300X300.Height / 2 - 89), Files.LogoWhite300X300);
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