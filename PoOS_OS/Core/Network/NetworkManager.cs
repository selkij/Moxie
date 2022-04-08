using System;
using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace ProjectOrizonOS.Core.Network
{
    internal class NetworkManager
    {

        public void DCHPConnect()
        {
            var xClient = new DHCPClient();

            try
            {
                xClient.SendDiscoverPacket();
                xClient.Close();
            } catch (Exception ex)
            {
                Kernel.shell.WriteLine("DHCP Discover failed. Can't apply dynamic IPv4 address. " + ex.ToString(), type: 3);
            }
        }

        public void ManualConnect(string networkDevice)
        {
            try
            {
                NetworkDevice nic = NetworkDevice.GetDeviceByName(networkDevice);

                IPConfig.Enable(nic, new Address(192, 168, 1, 69), new Address(255, 255, 255, 0), new Address(192, 168, 1, 254));

                Kernel.shell.WriteLine("Applied! IPv4: " + NetworkConfig.CurrentConfig.Value.IPAddress + " subnet mask: " + NetworkConfig.CurrentConfig.Value.SubnetMask + " gateway: " + NetworkConfig.CurrentConfig.Value.DefaultGateway);
            } catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
