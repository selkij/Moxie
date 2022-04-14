using System;
using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace Moxie.Core.Network
{
    internal class NetworkManager
    {

        public void DCHPConnect()
        {
            var xClient = new DHCPClient();

            try
            {
                Kernel.shell.Log("Initiating Network connection via DHCP...", 1);
                xClient.SendDiscoverPacket();
                var ip = NetworkConfig.CurrentConfig.Value.IPAddress;
                
                xClient.Close();

                if (ip.ToString() == "0.0.0.0")
                    Kernel.shell.Log($"DHCP Discover failed. IP set to {ip}", 3);
                else
                    Kernel.shell.Log("Etablished Network connection via DHCP IPv4: " + ip, 2);
            } catch (Exception ex)
            {
                Kernel.shell.WriteLine("DHCP Discover failed. Can't apply dynamic IPv4 address. " + ex, type: 3);
            }
        }

        public void ManualConnect(string networkDevice)
        {
            try
            {
                NetworkDevice nic = NetworkDevice.GetDeviceByName(networkDevice);

                IPConfig.Enable(nic, new Address(192, 168, 1, 69), new Address(255, 255, 255, 0), new Address(192, 168, 1, 254));
                var ip = NetworkConfig.CurrentConfig.Value.IPAddress;
                var sn = NetworkConfig.CurrentConfig.Value.SubnetMask;
                var gw = NetworkConfig.CurrentConfig.Value.DefaultGateway;

                Kernel.shell.WriteLine($"Applied! IPv4: {ip} subnet mask: {sn} gateway: {gw}");
            } catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}