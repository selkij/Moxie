using Cosmos.System.FileSystem.VFS;
using System;
using Cosmos.System.FileSystem.Listing;

namespace ProjectOrizonOS.Shell.Cmds.File
{
    internal class cCreateDir
    {
        private static ShellManager shell = new();

        public static void CreateDir(string file)
        {
            DirectoryEntry dir = VFSManager.GetDirectory(Kernel.current_directory);

            try
            {
                VFSManager.CreateDirectory(dir.mFullPath + file);
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
