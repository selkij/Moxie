using ProjectOrizonOS.Core.GUI;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;
using Files = ProjectOrizonOS.Resources.Files;

namespace ProjectOrizonOS.Application.Interface
{
    public class TextWindow : Window
    {
        public char[] Buffer = new char[80 * 25];
        
        public string DisplayName = "TextWindow";

        protected override void DrawFrame(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, 20, 0, Color: Color.White);

            DrawButtons(x, y);
            
            GuiManager.canvas.DrawString(x, y+2, DisplayName, Color.Black);
        }

        protected override void DrawContent(int x, int y)
        {
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, Height, 0, Color.Black);
        }

        protected override void DrawButtons(int x, int y)
        {
            GuiManager.canvas.DrawBitmap(x + Width - 25, y+2, Files.MaximizeButton);
            GuiManager.canvas.DrawBitmap(x + Width - 50, y+2, Files.CloseButton);
            GuiManager.canvas.DrawBitmap(x + Width - 75, y+2, Files.MinimizeButton);
        }
    }
}