using Sys = Cosmos.System;

namespace Moxie.Shell.Cmds
{
    internal class Shutdown : ICommand
    {
        public Shutdown(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute()
        {
            Sys.Power.Shutdown();
        }

        public override void Help()
        {
            Kernel.shell.WriteLine("shutdown/sb - Shutdowns Moxie");
        }
    }

    internal class Reboot : ICommand
    {
        public Reboot(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute()
        {
            Sys.Power.Reboot();
        }

        public override void Help()
        {
            Kernel.shell.WriteLine("reboot/rb - Reboots Moxie");
        }
    }
}