namespace ProjectOrizonOS.Shell.Cmds.Network
{
    class cIpConfig
    {

        private static ShellManager shell = new();

        public static void Help()
        {
            shell.WriteLine("Bad arguments detected. Possible arguments:", type: 3);
            shell.WriteLine("-- <type>   <arg2>     <arg3>   <arg4>");
            shell.WriteLine("-- /dhcp                                           asks a dynamic IPv4 via DHCP");
            shell.WriteLine("-- /manual <ipAddress> <subNet> <gw>               asks a dynamic IPv4 via DHCP");
        }
    }
}
