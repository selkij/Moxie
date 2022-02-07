using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;

namespace ProjectOrizonOS.Shell.Cmds.File
{
    internal class cRemoveDir
    {
        private static ShellManager shell = new();

        public static void RemoveDir(string dir)
        {
            try
            {
                DirectoryEntry temp = VFSManager.GetDirectory(Kernel.current_directory + dir);
            }
            catch (ArgumentNullException ex)
            {
                shell.WriteLine(ex + " Please specify a valid directory to delete", type: 3);
            }

            try
            {
                var directory_list = Directory.GetFiles(Kernel.current_directory + dir + @"\");

                if (directory_list.Length == 0)
                {
                    VFSManager.DeleteDirectory(Kernel.current_directory + dir, false);
                }
                else
                {
                    shell.WriteLine("Directory is not empty, cannot delete. use -r argument to delete the folder recursively");
                }

            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }

        public static void RemoveDirRecursively(string dir)
        {
            try
            {
                VFSManager.DeleteDirectory(@"0:\" + dir, true);
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
