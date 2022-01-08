using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    class cListDir
    {
        private static ShellManager shell = new();

        public static void ListDir()
        {
            try
            {
                var directory_list = VFSManager.GetDirectoryListing(Kernel.current_directory);

                foreach (var directoryEntry in directory_list)
                {
                    shell.WriteLine(directoryEntry.mName);
                }
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
