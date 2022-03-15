using Cosmos.System.FileSystem.VFS;
using System;

namespace ProjectOrizonOS.Shell.Cmds.File
{
    internal class cListDir
    {
        public static void ListDir()
        {
            try
            {
                var directory_list = VFSManager.GetDirectoryListing(Kernel.current_directory);

                foreach (var directoryEntry in directory_list)
                {
                    Kernel.shell.WriteLine(directoryEntry.mName);
                }
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
