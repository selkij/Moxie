using ProjectOrizonOS.Properties;
using ProjectOrizonOS.Shell;

namespace ProjectOrizonOS.Shell.Cmds.Console
{
    internal class cWhoAmI
    {
        public static void WhoAmI()
        {
            Kernel.shell.WriteLine(ShellInfo.user);
        }
    }
}
