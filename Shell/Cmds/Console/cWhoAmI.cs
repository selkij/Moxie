using SkippleOS.Shell;

namespace SkippleOS.Shell.Cmds.Console
{
    internal class cWhoAmI
    {

        private static ShellManager shell = new ShellManager();

        public static void WhoAmI()
        {
            shell.WriteLine(ShellInfo.user);
        }
    }
}
