using System.Drawing;
using ProjectOrizonOS.Core.GUI;
using ProjectOrizonOS.Libraries.Graphics;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;

namespace ProjectOrizonOS.Application.Interface
{
    public class TextWindow : Window
    {
        public char[] Buffer = new char[80 * 25];

        protected override void DrawFrame(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, 20, 0, Color: Color.White);
            //Add actions when clicked "Just check if it's within the x, y, width, and height of the Bitmap"
            GuiManager.canvas.DrawBitmap(x + Width - 25, y+2, GuiManager.maximizeButton);
            GuiManager.canvas.DrawBitmap(x + Width - 50, y+2, GuiManager.closeButton);
            
            

            GuiManager.canvas.DrawBitmap(x + Width - 75, y+2, GuiManager.minimizeButton);
            GuiManager.canvas.DrawString(x, y+2, DisplayName, Color.Black);
        }

        protected override void DrawContent(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, Height, 0, Color.Black);
        }

    }
}