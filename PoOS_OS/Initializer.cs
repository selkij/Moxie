using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Shell;
using ProjectOrizonOS.Shell.Network;
using System;
using Sys = Cosmos.System;

namespace ProjectOrizonOS
{
    class Initializer
    {

        private Sys.FileSystem.CosmosVFS fs;

        private ShellManager shell = new();
        private NetworkManager networkManager = new();

        public void vFS()
        {
            try
            {
                shell.Log("Initiating file system...", type: 1);
                fs = new Sys.FileSystem.CosmosVFS();
                VFSManager.RegisterVFS(fs);
            }
            catch (Exception ex)
            {
                shell.Write(ex.ToString(), type: 4);
                Console.ReadKey();
                Sys.Power.Shutdown();
            }
            shell.Log("File system initiated", type: 2);
        }

        public void DHCP()
        {
            shell.Log("Initiating Network connection via DHCP...", type: 1);
            bool skip = false;
            try
            {
                if (skip == false)
                {
                    networkManager.DCHPConnect();
                }
                else
                {
                    shell.Log("Process Skipped!", type: 2);
                }

            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}