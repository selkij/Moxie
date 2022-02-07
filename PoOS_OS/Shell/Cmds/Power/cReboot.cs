using Sys = Cosmos.System;

namespace ProjectOrizonOS.Shell.Cmds.Power
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
