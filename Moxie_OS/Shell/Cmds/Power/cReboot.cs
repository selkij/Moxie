using Sys = Cosmos.System;

namespace Moxie.Shell.Cmds.Power
{
    internal class cReboot
    {
        public static void Reboot()
        {
            System.Console.Clear();
            Sys.Power.Reboot();
        }
    }
}
