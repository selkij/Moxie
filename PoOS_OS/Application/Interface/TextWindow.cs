using System.Drawing;
using ProjectOrizonOS.Core.GUI;
using ProjectOrizonOS.Libraries.Graphics;

namespace ProjectOrizonOS.Application.Interface
{
    public class TextWindow : Window
    {
        public char[] Buffer = new char[80 * 25];

        protected override void DrawFrame(int x, int y)
        {
            Canvas.DrawFilledRectangle(x, y, Width, 20, Color.White);
            GuiManager.canvas.DrawBitmap(x + Width - 25, y+2, GuiManager.closeButton);
            GuiManager.canvas.DrawBitmap(x + Width - 50, y+2, GuiManager.maximizeButton);
            GuiManager.canvas.DrawBitmap(x + Width - 75, y+2, GuiManager.minimizeButton);
            Canvas.DrawString(x, y+2, DisplayName, Color.Black);
        }

        protected override void DrawContent(int x, int y)
        {
            Canvas.DrawFilledRectangle(x, y, Width, Height, Color.Black);
            
        }

    }
}