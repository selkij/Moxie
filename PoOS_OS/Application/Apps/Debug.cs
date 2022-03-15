using System;
using System.Drawing;
using Cosmos.System.Graphics;
using Canvas = ProjectOrizonOS.Libraries.Graphics.Canvas;
using Cosmos.Core;
using Cosmos.System.Graphics.Fonts;

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
            Canvas.DrawFilledRectangle(x, y, Width, 20, Color.White);
            Canvas.DrawString(x, y+2, DisplayName, Color.Black);
        }

        protected override void DrawContent(int x, int y)
        {
            //Background
            Canvas.DrawFilledRectangle(x, y, Width, Height, Color.FromArgb(245, 245, 245));
            
            Canvas.DrawString(x, y, "CPU name: " + CPU.GetCPUBrandString(), Color.Black);
            Canvas.DrawString(x, y + PCScreenFont.Default.Height + 2, "CPU brand: " + CPU.GetCPUVendorName(), Color.Black);
            Canvas.DrawString(x, y + PCScreenFont.Default.Height * 2 + 2, "CPU cycle speed: " + CPU.GetCPUCycleSpeed(), Color.Black);
            Canvas.DrawString(x, y+ PCScreenFont.Default.Height * 3 + 2, $"Used RAM: {GCImplementation.GetUsedRAM() / 1024 / 1024}MB / {GCImplementation.GetAvailableRAM()}MB  ({ramUsage}%)", Color.Black);
        }
    }
}