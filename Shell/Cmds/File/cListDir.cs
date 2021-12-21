using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    class cListDir
    {
        private static ShellManager shell = new ShellManager();

        public static void ListDir()
        {
            try
            {
                var directory_list = VFSManager.GetDirectoryListing(@"0:\");

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
