using ProjectOrizonOS.Core.GUI;
using ProjectOrizonOS.Libraries.Graphics;
using Cosmos.System;
using Files = ProjectOrizonOS.Resources.Files;

namespace ProjectOrizonOS.Application.Interface
{
    public class GraphicsWindow : Window
    {
        private string DisplayName = "GraphicsWindow";
        
        protected override void DrawFrame(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, 20, 0, Color.White);
            
            //Frame
            if (MouseManager.X > x && MouseManager.X < x + Width && MouseManager.Y > y && MouseManager.Y < y + 20)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    //TODO: Moving Frame with Content
                }
            }
            
            DrawButtons(x, y);
            
            GuiManager.canvas.DrawString(x, y+2, DisplayName, Color.Black);
        }

        protected override void DrawContent(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, Height, 0, Color.Black);
            GuiManager.canvas.DrawFilledCircle(Width/2, Height/2, 10, Color.White);
        }

        protected override void DrawButtons(int x, int y)
        {
            GuiManager.canvas.DrawBitmap(x + Width - 25, y+2, Files.MaximizeButton);
            
            if (MouseManager.X > x + Width - 50 && MouseManager.X < x + Width - 50 + Width && MouseManager.Y > y+2 && MouseManager.Y < y+2 + Files.CloseButton.Height)
            {
                GuiManager.canvas.DrawFilledCircle(x + Width - 50, y+2, 1, new Color(50, 45, 143, 211));
                
                if (MouseManager.MouseState == MouseState.Left)
                {
                    GuiManager.canvas.DrawString(x + Width - 50, y+2, "Test", new Color(50, 45, 143, 211));
                }
            }
            
            GuiManager.canvas.DrawBitmap(x + Width - 50, y+2, Files.CloseButton);

            GuiManager.canvas.DrawBitmap(x + Width - 75, y+2, Files.MinimizeButton);
            
            
            
        }
    }
}