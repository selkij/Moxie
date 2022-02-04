using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using System;

namespace ProjectOrizonOS.Shell.Network
{
    internal class NetworkManager
    {
        private ShellManager shell = new();

        public DHCPClient xClient = new();

        public string DCHPConnect()
        {
            try
            {
                xClient.SendDiscoverPacket();
                shell.WriteLine("Network connection ethablished via DHCP with IPv4: " + NetworkConfig.CurrentConfig.Value.IPAddress.ToString(), type:2);
                return null;
            } catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
                return ex.ToString();
            }
        }

    }
}
