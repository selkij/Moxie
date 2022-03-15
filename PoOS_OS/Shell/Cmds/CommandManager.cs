using ProjectOrizonOS.Shell.Cmds.Power;
using ProjectOrizonOS.Shell.Cmds.Console;
using ProjectOrizonOS.Shell.Cmds.File;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Network.Config;
using System;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using ProjectOrizonOS.Core.Network;
using ProjectOrizonOS.Shell.Cmds.Network;

namespace ProjectOrizonOS.Shell.Cmds
{
    class CommandManager
    {
        private NetworkManager networkManager = new();
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
                    if (cmd[1] == ".." && Kernel.current_directory != @"0:\")
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
                    if (string.IsNullOrWhiteSpace(cmd[1]))
                    {
                        Kernel.shell.WriteLine("Please choose a file to output", type: 3);
                    }
                    else
                    {
                        cCat.Cat(cmd[1]);
                    }
                    break;


                #endregion

                #region Network
                case "ipinfo":
                    try
                    {
                        Kernel.shell.WriteLine(NetworkConfig.CurrentConfig.Value.IPAddress.ToString());
                    }
                    catch (Exception ex)
                    {
                        Kernel.shell.WriteLine(ex.ToString(), type: 3);
                    }

                    break;

                case "ipconfig":
                    if (!string.IsNullOrWhiteSpace(cmd[1]) && cmd[1] == "/dhcp")
                    {
                        networkManager.DCHPConnect();
                    } else if (!string.IsNullOrWhiteSpace(cmd[1]) && !string.IsNullOrWhiteSpace(cmd[2]) && cmd[1] == "/manual")
                    {
                        networkManager.ManualConnect(cmd[2]);
                    }
                    break;
                #endregion

                default:
                    Kernel.shell.WriteLine("Unknown command. Please type \'help\' to see the commands", type: 3);
                    break;
            }
        }
    }
}
