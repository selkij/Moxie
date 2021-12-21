using Sys = Cosmos.System;

namespace SkippleOS.Shell.Cmds.Power
{
    public class cShutdown
    {
        public static void Shutdown()
        {
            System.Console.Clear();
            Sys.Power.Shutdown();
        }
        
    }
}
