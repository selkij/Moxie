namespace ProjectOrizonOS.Shell.Cmds.Network
{
    class cIpConfig
    {
        public static void Help()
        {
            Kernel.shell.WriteLine("Bad arguments detected. Possible arguments:", type: 3);
            Kernel.shell.WriteLine("-- <type>   <arg2>     <arg3>   <arg4>");
            Kernel.shell.WriteLine("-- /dhcp                                           asks a dynamic IPv4 via DHCP");
            Kernel.shell.WriteLine("-- /manual <ipAddress> <subNet> <gw>               asks a dynamic IPv4 via DHCP");
        }
    }
}
