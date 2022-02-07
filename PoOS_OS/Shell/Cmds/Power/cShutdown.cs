using Sys = Cosmos.System;

namespace ProjectOrizonOS.Shell.Cmds.Power
{
    internal class cShutdown
    {
        public static void Shutdown()
        {
            System.Console.Clear();
            Sys.Power.Shutdown();
        }
    }
}
