using System;
using Sys = Cosmos.System;

namespace SkippleOS.Shell
{
    internal class ShellManager
    {

        /// <summary>
        /// Output text with color 
        /// </summary>
        /// <param name="text">The text to output</param>
        /// <param name="foregroundColor">Change foreground text color</param>
        /// <param name="backgroundColor">Change background text color</param>
        /// <param name="type">Type of text 1:Process 2:Success 3:Error</param>
        public void Write(string text, ConsoleColor foregroundColor=ConsoleColor.White, ConsoleColor backgroundColor=ConsoleColor.Black, int type=0)
        {
            switch(type)
            {
                case 0:
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                //Process
                case 1:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("PROCESS:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                //Success
                case 2:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("SUCCESS:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                //Error
                case 3:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("ERROR:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }

        /// <summary>
        /// Output text with color 
        /// </summary>
        /// <param name="text">The text to output</param>
        /// <param name="foregroundColor">Change foreground text color</param>
        /// <param name="backgroundColor">Change background text color</param>
        /// <param name="type">Type of text 1:Process 2:Success 3:Error</param>
        public void WriteLine(string text, ConsoleColor foregroundColor=ConsoleColor.White, ConsoleColor backgroundColor=ConsoleColor.Black, int type=0)
        {
            switch (type)
            {
                case 0:
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.WriteLine(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                //Process
                case 1:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("PROCESS:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                //Success
                case 2:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("SUCCESS:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                //Error
                case 3:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("ERROR:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
