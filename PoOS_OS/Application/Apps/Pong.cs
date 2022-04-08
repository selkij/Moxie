using ProjectOrizonOS.Core.GUI;
using ProjectOrizonOS.Libraries.Graphics;
using Files = ProjectOrizonOS.Resources.Files;

namespace ProjectOrizonOS.Application.Apps
{
    public class Pong : Window
    {
        public char[] Buffer = new char[80 * 25];

        private string DisplayName = "Pong!";
        
        protected override void DrawFrame(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, 20, 0, Color: Color.White);
            
            DrawButtons(x, y);
            
            GuiManager.canvas.DrawString(x, y+2, DisplayName, Color.Black);
        }

        protected override void DrawContent(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, Height, 0, Color.Black);
            GuiManager.canvas.DrawFilledRectangle(x+5, Height / 2, 5, 25, 0, Color.White);
            GuiManager.canvas.DrawFilledRectangle(x+5, Height / 2, 5, 25, 0, Color.White);
            GuiManager.canvas.DrawFilledCircle(Width/2, Height/2, 10, Color.White);
        }

        protected override void DrawButtons(int x, int y)
        {
            GuiManager.canvas.DrawBitmap(x + Width - 25, y+2, Files.MaximizeButton);
            GuiManager.canvas.DrawBitmap(x + Width - 50, y+2, Files.CloseButton);
            GuiManager.canvas.DrawBitmap(x + Width - 75, y+2, Files.MinimizeButton);
        }
    }
}