using SkippleOS.Shell.Cmds.Power;
using SkippleOS.Shell.Cmds.Console;
using SkippleOS.Shell.Cmds.File;
using System;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;

namespace SkippleOS.Shell
{
    internal class ShellManager
    {
        #region Write
        /// <summary>
        /// Output text with color 
        /// </summary>
        /// <param name="text">The text to output</param>
        /// <param name="foregroundColor">Change foreground text color</param>
        /// <param name="backgroundColor">Change background text color</param>
        /// <param name="type">Type of text 1:Process 2:Success 3:Error 4:Fatal</param>
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

                //Fatal
                case 4:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("FATAl:");
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
        /// <param name="type">Type of text 1:Process 2:Success 3:Error 4:Fatal</param>
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

                //Fatal
                case 4:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("FATAl:");
                    Console.ForegroundColor = foregroundColor;
                    Console.BackgroundColor = backgroundColor;
                    Console.Write(" " + text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
        #endregion

        #region ExecuteCommand
        public void ExecuteCommand(string[] cmd)
        {
            var cName = cmd[0];

            switch (cName)
            {
                #region Power
                case "shutdown":
                case "sd":
                    cShutdown.Shutdown();
                    break;

                case "reboot":
                case "rb":
                    cReboot.Reboot();
                    break;
                #endregion

                #region Console
                case "clear":
                case "cls":
                    cClear.Clear();
                    break;

                case "whoami":
                    cWhoAmI.WhoAmI();
                    break;

                case "echo":
                    cEcho.Echo(cmd);
                    break;

                case "setKeyboardMap":
                    cKeyboardMap.SetKeyboardMap(cmd[1]);
                    break;

                case "help":
                    cHelp.Help();
                    break;
                #endregion

                #region File
                case "cd":
                    cCD.CD(cmd[1]);
                    if(cmd[1] == ".." && Kernel.current_directory != @"0:\")
                    {
                        DirectoryEntry folder = VFSManager.GetDirectory(Kernel.current_directory);
                        Kernel.current_directory = folder.mParent.mFullPath;
                    }
                    
                    break;

                case "listdir":
                case "ls":
                    cListDir.ListDir();
                    break;

                case "mkfile":
                    cCreateFile.CreateFile(cmd[1]);
                    break;

                case "mkdir":
                    cCreateDir.CreateDir(cmd[1]);
                    break;

                case "rmfile":
                    cRemoveFile.RemoveFile(cmd[1]);
                    break;

                case "rmdir":
                    cRemoveDir.RemoveDirRecursively(cmd[1]);
                    break;

                case "cat":
                    if(cmd[1] == null)
                    {
                        WriteLine("Please choose a file to output", type: 3);
                    }else
                    {
                        cCat.Cat(cmd[1]);
                    }
                    break;
                #endregion

                default:
                    WriteLine("Unknown command. Please type \'help\' to see the commands", type: 3);
                    break;
            }
        }
        #endregion
    }
}
