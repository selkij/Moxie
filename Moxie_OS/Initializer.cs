using Cosmos.System.FileSystem.VFS;
using System;
using Moxie.Core.Network;
using Sys = Cosmos.System;

namespace Moxie
{
    class Initializer
    {

        private Sys.FileSystem.CosmosVFS fs;
        private NetworkManager networkManager = new();

        public void vFS()
        {
            try
            {
                Kernel.shell.Log("Initiating file system...", 1);
                fs = new Sys.FileSystem.CosmosVFS();
                VFSManager.RegisterVFS(fs);
            }
            catch (Exception ex)
            {
                Kernel.shell.Log(ex.ToString(), 3);
                Console.ReadKey();
                Sys.Power.Shutdown();
            }
            Kernel.shell.Log("File system initiated", 2);
        }

        public void DHCP()
        {
            bool skip = false;

            if (skip == false)
            {
                networkManager.DCHPConnect();
            }
            else
            {
                Kernel.shell.Log("Process Skipped!", type: 2);
            }
        }
    }
}
