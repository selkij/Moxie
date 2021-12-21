using Sys = Cosmos.System;

namespace SkippleOS.Shell.Cmds.Power
{
    class cReboot
    {
        public static void Reboot()
        {
            System.Console.Clear();
            Sys.Power.Reboot();
        }
    }
}
