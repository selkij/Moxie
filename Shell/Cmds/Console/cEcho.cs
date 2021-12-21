namespace SkippleOS.Shell.Cmds.Console
{
    internal class cEcho
    {

        private static ShellManager shell = new ShellManager();

        public static void Echo()
        {
            var input = System.Console.ReadLine();
            shell.Write(input);
        }
    }
}
