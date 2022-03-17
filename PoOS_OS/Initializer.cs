using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Shell;
using System;
using Cosmos.System.Network.Config;
using ProjectOrizonOS.Core.Network;
using Sys = Cosmos.System;

namespace ProjectOrizonOS
{
    class Initializer
    {

        private Sys.FileSystem.CosmosVFS fs;
        private NetworkManager networkManager = new();

        public void vFS()
        {
            try
            {
                Kernel.shell.Log("Initiating file system...", type: 1);
                fs = new Sys.FileSystem.CosmosVFS();
                VFSManager.RegisterVFS(fs);
            }
            catch (Exception ex)
            {
                Kernel.shell.Write(ex.ToString(), type: 4);
                Console.ReadKey();
                Sys.Power.Shutdown();
            }
            Kernel.shell.Log("File system initiated", type: 2);
        }

        public void DHCP()
        {
            Kernel.shell.Log("Initiating Network connection via DHCP...", type: 1);
            bool skip = false;
            try
            {
                if (skip == false)
                {
                    networkManager.DCHPConnect();
                    Kernel.shell.Log("Etablished Network connection via DHCP IPv4: " + NetworkConfig.CurrentConfig.Value.IPAddress, 2);
                }
                else
                {
                    Kernel.shell.Log("Process Skipped!", type: 2);
                }

            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}