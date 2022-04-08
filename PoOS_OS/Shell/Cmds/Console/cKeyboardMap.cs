using Sys = Cosmos.System;
using Cosmos.System.ScanMaps;
using System;
using ProjectOrizonOS.Properties;

namespace ProjectOrizonOS.Shell.Cmds.Console
{
    internal class cKeyboardMap
    {
        public static void SetKeyboardMap(string keyMap)
        {

            switch(keyMap)
            {
                case "en_US":
                    if(ShellInfo.langSelected != "en_US")
                    {
                        Sys.KeyboardManager.SetKeyLayout(new US_Standard());
                        ShellInfo.langSelected = "en_US";
                    } else
                    {
                        Kernel.shell.WriteLine("The Keyboard mapping is already on en_US.",type: 3);
                    }
                    break;

                case "fr_FR":
                    if(ShellInfo.langSelected != "fr_FR")
                    {
                        Sys.KeyboardManager.SetKeyLayout(new FR_Standard());
                        ShellInfo.langSelected = "fr_FR";
                    }
                    else
                    {
                        Kernel.shell.WriteLine("The Keyboard mapping is already on fr_FR.", type: 3);
                    }
                    break;

                case "en_DE":
                    if (ShellInfo.langSelected != "en_DE")
                    {
                        Sys.KeyboardManager.SetKeyLayout(new DE_Standard());
                        ShellInfo.langSelected = "en_DE";
                    }
                    else
                    {
                        Kernel.shell.WriteLine("The Keyboard mapping is already on en_DE.", type: 3);
                    }
                    break;

                default:
                    Kernel.shell.Write("Please enter a valid keyboard language. Example: \"en_US\"", type: 3);
                    break;
            }
        }
    }
}
