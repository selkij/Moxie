using System.Drawing;
using Cosmos.HAL;
using ProjectOrizonOS.Libraries.Graphics;
using ProjectOrizonOS.Properties;

namespace ProjectOrizonOS.Core.GUI
{
    public class Desktop
    {
        public void Draw()
        {
            string time = $"{RTC.DayOfTheMonth} {RTC.Hour}:{RTC.Minute}.{RTC.Second}";
                        
            Canvas.Clear(Color.FromArgb(254, 86, 20, 142));

            //canvas.DrawBitmap(0, 0, wallpaper1024x768);
                        
            //Top Bar
            Canvas.DrawFilledRectangle(0, 0, (int) GuiManager.screenWidth, 20, Color.White);
            Canvas.DrawString((int) GuiManager.screenWidth / 2 - time.Length, 3, time, Color.Black);
                        
            //Bottom Bar
            Canvas.DrawFilledRectangle(0, (int) GuiManager.screenHeight - 20, (int) GuiManager.screenWidth, 20, Color.Black);
            Canvas.DrawString(2, (int) GuiManager.screenHeight - 18, $"ProjectOrizonOS Dev V.{ShellInfo.version}  fps={Canvas.FPS}" , Color.White);
        }
    }
}