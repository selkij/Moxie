using Moxie.Properties;

namespace Moxie.Shell.Cmds.Console
{
    internal class cWhoAmI
    {
        public static void WhoAmI()
        {
            Kernel.shell.WriteLine(Info.user);
        }
    }
}