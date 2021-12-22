using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    internal class cRemoveFile
    {

        private static ShellManager shell = new ShellManager();

        public static void RemoveFile(string file)
        {
            try
            {
                DirectoryEntry temp = VFSManager.GetDirectory(ShellInfo.current_directory + file);
            }
            catch (ArgumentNullException ex)
            {
                shell.WriteLine(ex + " Please specify a valid file to delete", type: 3);
            }

            try
            {
                VFSManager.DeleteFile(ShellInfo.current_directory + file);
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
