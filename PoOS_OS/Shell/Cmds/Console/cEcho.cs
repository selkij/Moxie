namespace ProjectOrizonOS.Shell.Cmds.Console
{
    internal class cEcho
    {
        public static void Echo(string[] msg)
        {
            for(int i = 1; i < msg.Length; i++)
            {
                Kernel.shell.Write(msg[i]+ " ");
            }
            
        }
    }
}
