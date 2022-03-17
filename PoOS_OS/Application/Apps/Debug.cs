using System;
using Canvas = ProjectOrizonOS.Libraries.Graphics.Canvas;
using Cosmos.Core;
using Cosmos.System.Graphics.Fonts;
using ProjectOrizonOS.Core.GUI;
using Sys = Cosmos.System;
using Color = ProjectOrizonOS.Libraries.Graphics.Color;

namespace ProjectOrizonOS.Application.Apps
{
    public class Debug : Window
    {
        //App properties
        public string DisplayName = "Debug";
        public int OffsetX = 50, OffsetY = 50;
        public int Width = 500, Height = 250;
        
        //Percentage of RAM usage
        public int ramUsage = (int)Math.Round((double)(100 * (GCImplementation.GetUsedRAM() / 1024 / 1024)) / GCImplementation.GetAvailableRAM());

        protected override void DrawFrame(int x, int y)
        {
            //Frame
            GuiManager.canvas.DrawFilledRectangle(x, y, Width, 20, 0, Color.White);
            GuiManager.canvas.DrawBitmap(x + Width - 25, y+2, GuiManager.closeButton);
            
            GuiManager.canvas.DrawBitmap(x + Width - 50, y+2, GuiManager.maximizeButton);
            GuiManager.canvas.DrawBitmap(x + Width - 75, y+2, GuiManager.minimizeButton);
            GuiManager.canvas.DrawString(x, y+2, DisplayName, Color.Black);
            
            if (Sys.MouseManager.X > x + Width - 25 && Sys.MouseManager.X < x + Width - 25 && Sys.MouseManager.Y > y+2 && Sys.MouseManager.Y < y+2)
            {
                if (Kernel.guiManager.pressed)
                {
                    AppManager.RemoveApp(Kernel.guiManager.debug);
                    GuiManager.canvas.DrawString(x + Width / 2, y+2, "YEET", Color.Black);
                }
            }
        }

        protected override void DrawContent(int x, int y)
        {
            unsafe
            {
                //Background
                GuiManager.canvas.DrawFilledRectangle(x, y, Width, Height, 0, Color.SystemColors.BackGround);
            
                GuiManager.canvas.DrawString(x, y, "CPU name: " + CPU.GetCPUBrandString(), Color.White);
                GuiManager.canvas.DrawString(x, y + PCScreenFont.Default.Height + 2, "CPU brand: " + CPU.GetCPUVendorName(), Color.White);
                GuiManager.canvas.DrawString(x, y + PCScreenFont.Default.Height * 2 + 2, $"CPU speed: {CPU.GetCPUCycleSpeed() / 1E+09}GHz", Color.White);
                GuiManager.canvas.DrawString(x, y+ PCScreenFont.Default.Height * 4 + 2, $"Used RAM: {GCImplementation.GetUsedRAM() / 1024 / 1024}MB / {GCImplementation.GetAvailableRAM()}MB  ({ramUsage}%)", Color.White);
                GuiManager.canvas.DrawString(x, y+ PCScreenFont.Default.Height * 5 + 2, $"VBE version: {VBE.getControllerInfo().vbeVersion}", Color.White);
            }
        }
    }
}