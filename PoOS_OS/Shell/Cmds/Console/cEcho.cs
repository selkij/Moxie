namespace ProjectOrizonOS.Shell.Cmds.Console
{
    internal class cEcho
    {

        private static ShellManager shell = new();

        public static void Echo(string[] msg)
        {
            for(int i = 1; i < msg.Length; i++)
            {
                shell.Write(msg[i]+ " ");
            }
            
        }
    }
}
