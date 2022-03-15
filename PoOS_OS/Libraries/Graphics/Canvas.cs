using System;
using System.Drawing;
using System.IO;
using System.Text;
using Cosmos.Core;
using Cosmos.System.Graphics;
using VBE = ProjectOrizonOS.Libraries.Graphics.VBE;

namespace ProjectOrizonOS.Libraries.Graphics
{
    public unsafe class Canvas
    {
        public static int Width, Height;
        public static float AspectRatio;
        public Canvas()
        {
            if (VBE.Ininitialized)
            {
                Width = VBE.Width;
                Height = VBE.Height;
            }
            else
            {
                throw new Exception("Canavs: VBE was not ininitialized");
            }
        }

        #region Pixel

        public static void SetPixel(int x, int y, Color color)
        {
            // Draw the pixel
            VBE.Buffer[(Width * y) + x] = (int*)color.ToArgb();
        }
        public static void DrawPixel(int X, int Y, Color Color)
        {
            if (X < 0 || X >= Width || Y < 0 || Y >= Height || Color.A == 0)
                return;
            if (Color.A < 255)
                Color = Blend(GetPixel(X, Y), Color);

            // Draw the pixel
            VBE.Buffer[(Width * Y) + X] = (int*)Color.ToArgb();
        }
        public static Color GetPixel(int X, int Y)
        {
            return Color.FromArgb((int)VBE.Buffer[(Width * Y) + X]);
        }
        public static Color Blend(Color Back, Color Front)
        {
            int R = ((Front.A * Front.R) + ((256 - Front.A) * Back.R)) >> 8;
            int G = ((Front.A * Front.G) + ((256 - Front.A) * Back.G)) >> 8;
            int B = ((Front.A * Front.B) + ((256 - Front.A) * Back.B)) >> 8;
            return Color.FromArgb(R, G, B);
        }

        #endregion

        #region Line

        public static void DrawHLine(int x, int y, int length, Color color)
        {
            if (x + length > Width)
                length = Width - x;

            for (int i = 0; i < length; i++)
            {
                DrawPixel(x + i, y, color);
            }
        }

        public static void DrawVLine(int x, int y, int length, Color color)
        {
            if (y + length > Height)
                length = Height - y;

            for (int i = 0; i < length; i++)
            {
                DrawPixel(x, y + i, color);
            }
        }

        public static void DrawLine(int X, int Y, int X2, int Y2, Color Color)
        {
            int dx = Math.Abs(X2 - X), sx = X < X2 ? 1 : -1;
            int dy = Math.Abs(Y2 - Y), sy = Y < Y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;

            while (X != X2 || Y != Y2)
            {
                DrawPixel(X, Y, Color);
                int e2 = err;
                if (e2 > -dx) { err -= dy; X += sx; }
                if (e2 < dy) { err += dx; Y += sy; }
            }
        }
        public static void DrawAngledLine(int X, int Y, int Angle, int Radius, Color Color)
        {
            DrawLine(X, Y, (int)(X + (Math.Cos(Math.PI * Angle / 180) * Radius)), (int)(X + (Math.Sin(Math.PI * Angle / 180) * Radius)), Color);
        }

        #endregion

        #region Rectangle

        public static void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            DrawHLine(x, y, width, color); // top Line
            DrawHLine(x, y + height - 1, width, color); // bottom line
            DrawVLine(x, y, height, color); // top Line
            DrawVLine(x + width - 1, y, height, color); // bottom line
        }

        public static void DrawFilledRectangle(int x, int y, int width, int height, Color color)
        {
            if (x + width >= Width)
                width = Width - x;
            if (y + height >= Height)
                height = Height - y;

            for (int i = y; i < y + height; i++)
            {
                for (int j = x; j < x + width; j++)
                {
                    SetPixel(j, i, color);
                }
            }
            return;
        }

        #endregion

        #region Circle

        public static void DrawCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0 || StartAngle == EndAngle)
                return;

            for (; StartAngle < EndAngle; StartAngle++)
            {
                DrawPixel(
                    X: (int)(X + (Radius * Math.Sin(Math.PI * StartAngle / 180))),
                    Y: (int)(Y + (Radius * Math.Cos(Math.PI * StartAngle / 180))),
                    Color: Color);
            }
        }

        public static void DrawFilledCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0 || StartAngle == EndAngle)
                return;

            for (int I = 0; I < Radius; I++)
            {
                DrawCircle(X, Y, I, Color, StartAngle, EndAngle);
            }
        }

        #endregion

        #region Triangle

        public static void DrawTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            DrawLine(X1, Y1, X2, Y2, Color);
            DrawLine(X1, Y1, X3, Y3, Color);
            DrawLine(X2, Y2, X3, Y3, Color);
        }
        public static void DrawFilledTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            // TODO: optimise and fix
            int dx = Math.Abs(X2 - X1), sx = X1 < X2 ? 1 : -1;
            int dy = Math.Abs(Y2 - Y1), sy = Y1 < Y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;

            while (X1 != X2 || Y1 != Y2)
            {
                DrawLine(X1, Y1, X3, Y3, Color);
                int e2 = err;
                if (e2 > -dx) { err -= dy; X1 += sx; }
                if (e2 < dy) { err += dx; Y1 += sy; }
            }
        }

        #endregion

        #region Image
        
        public void DrawBitmap(int X, int Y, Bitmap Bitmap)
        {
            for (int IX = 0; IX < Bitmap.Width; IX++)
            {
                for (int IY = 0; IY < Bitmap.Height; IY++)
                    DrawPixel(X + IX, Y + IY, Color.FromArgb(Bitmap.rawData[(Bitmap.Width * IY) + IX]));
            }
        }

        #endregion

        #region Text

        public static void DrawChar(int x, int y, char ch, Color Color)
        {
            if (ch == ' ') return;

            Font.Default.MS.Seek((Encoding.ASCII.GetBytes(ch.ToString())[0] & 0xFF) * Font.Default.Height, SeekOrigin.Begin);
            byte[] fontbuf = new byte[Font.Default.Height];
            Font.Default.MS.Read(fontbuf, 0, fontbuf.Length);

            for (int IY = 0; IY < Font.Default.Height; IY++)
            {
                for (int IX = 0; IX < Font.Default.Width; IX++)
                {
                    if ((fontbuf[IY] & (0x80 >> IX)) != 0)
                        DrawPixel(x + IX, y + IY, Color);
                }
            }
        }

        public static void DrawString(int X, int Y, string Text, Color Color)
        {
            string[] Lines = Text.Split('\n');
            for (int Line = 0; Line < Lines.Length; Line++)
            {
                for (int Char = 0; Char < Lines[Line].Length; Char++)
                {
                    Font.Default.MS.Seek((Encoding.ASCII.GetBytes(Lines[Line][Char].ToString())[0] & 0xFF) * Font.Default.Height, SeekOrigin.Begin);
                    byte[] fontbuf = new byte[Font.Default.Height];
                    Font.Default.MS.Read(fontbuf, 0, fontbuf.Length);

                    for (int IY = 0; IY < Font.Default.Height; IY++)
                    {
                        for (int IX = 0; IX < Font.Default.Width; IX++)
                        {
                            if ((fontbuf[IY] & (0x80 >> IX)) != 0)
                                DrawPixel(X + IX + (Char * Font.Default.Width), Y + IY + (Line * Font.Default.Height), Color);
                        }
                    }
                }
            }
        }
        #endregion

        #region Misc

        public static void Clear(Color Color = default)
        {
            if (Color == default)
            {
                Color = Color.Black;
            }

            if (Color.A == 255)
            {
                MemoryOperations.Fill((int[])(object)VBE.Buffer, Color.ToArgb());
            }
            else
            {
                for (int X = 0; X < Width; X++)
                {
                    for (int Y = 0; Y < Height; Y++)
                    {
                        DrawPixel(X, Y, Color);
                    }
                }
            }
        }

        int lastSecond = 0;
        int frames = 0;
        public static int FPS;
        public void Update()
        {           
            if (lastSecond != Cosmos.HAL.RTC.Second)
            {
                FPS = frames;
                lastSecond = Cosmos.HAL.RTC.Second;
                frames = 0;
            }                

            Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy((int[])(object)VBE.Buffer);

            frames++;
        }

        #endregion
    }
}