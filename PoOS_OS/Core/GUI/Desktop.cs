using System.Drawing;
using Cosmos.HAL;
using ProjectOrizonOS.Libraries.Graphics;
using ProjectOrizonOS.Properties;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;

namespace ProjectOrizonOS.Core.GUI
{
    public class Desktop
    {
        public void Draw()
        {
            string time = $"{RTC.DayOfTheMonth} {RTC.Hour}:{RTC.Minute}.{RTC.Second}";
                        
            GuiManager.canvas.Clear(new Color(254, 86, 20, 142));

            //canvas.DrawBitmap(0, 0, wallpaper1024x768);
                        
            //Top Bar
            GuiManager.canvas.DrawFilledRectangle(0, 0, (int) GuiManager.screenWidth, 20, 0, Color.White);
            GuiManager.canvas.DrawString((int) GuiManager.screenWidth / 2 - time.Length, 3, time, Color.Black);
                        
            //Bottom Bar
            GuiManager.canvas.DrawFilledRectangle(0, (int) GuiManager.screenHeight - 20, (int) GuiManager.screenWidth, 20, 0, Color.Black);
            GuiManager.canvas.DrawString(2, (int) GuiManager.screenHeight - 18, $"ProjectOrizonOS Dev V.{ShellInfo.version}  fps={GuiManager.canvas.FPS}" , Color.White);
        }
    }
}