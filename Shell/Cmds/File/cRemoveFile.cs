using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    internal class cRemoveFile
    {

        private static ShellManager shell = new();

        public static void RemoveFile(string file)
        {
            try
            {
                VFSManager.DeleteFile(Kernel.current_directory + file);
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
