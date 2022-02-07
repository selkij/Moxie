using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using System;

namespace ProjectOrizonOS.Shell.Network
{
    internal class NetworkManager
    {
        private ShellManager shell = new();

        public void DCHPConnect()
        {
            var xClient = new DHCPClient();
            if (xClient.SendDiscoverPacket() != -1)
            {
                xClient.Close();
                shell.WriteLine("Configuration applied! Your local IPv4 Address is .");
            }
            else
            {
                xClient.Close();
                shell.WriteLine("DHCP Discover failed. Can't apply dynamic IPv4 address.", type: 3);
            }
        }
    }
}
