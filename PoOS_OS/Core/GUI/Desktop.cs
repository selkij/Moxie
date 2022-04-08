using Cosmos.HAL;
using ProjectOrizonOS.Properties;
using ProjectOrizonOS.Resources;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;

namespace ProjectOrizonOS.Core.GUI
{
    public class Desktop
    {
        private Color bgColor = new(254, 168, 127, 63);
        
        public void Draw()
        {
            string time = $"{RTC.DayOfTheMonth} {RTC.Hour}:{RTC.Minute}.{RTC.Second}";
                        
            GuiManager.canvas.Clear(bgColor);

            //GuiManager.canvas.DrawBitmap(0, 0, Files.Wallpaper1024X768);
            
            GuiManager.canvas.DrawBitmap((int) (GuiManager.canvas.Width / 2 - Files.LogoWhite300X300.Width / 2), (int) (GuiManager.canvas.Height / 2 - Files.LogoWhite300X300.Height / 2 - 89), Files.LogoWhite300X300);
                        
            //Top Bar
            GuiManager.canvas.DrawFilledRectangle(0, 0, (int) GuiManager.screenWidth, 20, 0, Color.White);
            GuiManager.canvas.DrawString((int) GuiManager.screenWidth / 2 - time.Length, 3, time, Color.Black);
                        
            //Bottom Bar
            GuiManager.canvas.DrawFilledRectangle(0, (int) GuiManager.screenHeight - 20, (int) GuiManager.screenWidth, 20, 0, Color.Black);
            GuiManager.canvas.DrawString(2, (int) GuiManager.screenHeight - 18, $"ProjectOrizonOS Dev V.{ShellInfo.version}  fps={GuiManager.canvas.FPS}" , Color.White);
        }
    }
}