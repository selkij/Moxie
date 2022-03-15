using Sys = Cosmos.System;

namespace ProjectOrizonOS.Core.GUI
{
    public class Cursor
    {
        public void Initialize(uint screenWidth, uint screenHeight)
        {
            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;
        }
            
        public void DrawCursor()
        {
            GuiManager.canvas.DrawBitmap((int) Sys.MouseManager.X, (int) Sys.MouseManager.Y, GuiManager.cursorImage);
            
            
        }

    }
}