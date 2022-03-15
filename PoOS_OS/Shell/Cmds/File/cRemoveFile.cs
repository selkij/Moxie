using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;

namespace ProjectOrizonOS.Shell.Cmds.File
{
    internal class cRemoveFile
    {
        public static void RemoveFile(string file)
        {
            try
            {
                VFSManager.DeleteFile(Kernel.current_directory + file);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
