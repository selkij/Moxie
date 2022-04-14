using System;
using System.IO;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;

namespace Moxie.Shell.Cmds.File
{
    internal class cRemoveDir
    {

        public static void RemoveDir(string dir)
        {
            if (!dir.EndsWith(@"\")) dir = dir + @"\";
            try
            {
                DirectoryEntry temp = VFSManager.GetDirectory(Kernel.CurrentDirectory + dir);
            }
            catch (ArgumentNullException ex)
            {
                Kernel.shell.WriteLine(ex + " Please specify a valid directory to delete", type: 3);
            }

            try
            {
                var directory_list = Directory.GetFiles(Kernel.CurrentDirectory + dir);

                Directory.Delete(Kernel.CurrentDirectory + dir);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
