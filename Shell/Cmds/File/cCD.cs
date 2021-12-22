using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    internal class cCD
    {

        private static ShellManager shell = new ShellManager();

        public static void CD(string aPath)
        {
            bool exception = false;
            try
            {
                DirectoryEntry folder = VFSManager.GetDirectory(ShellInfo.current_directory + aPath);
                exception = true;
            } catch (ArgumentNullException ex)
            {
                shell.WriteLine(ex.ToString() + "Please enter a valid path.", type: 3);
            }

            if(!exception)
            {
                DirectoryEntry folder = VFSManager.GetDirectory(ShellInfo.current_directory + aPath);
                ShellInfo.current_directory = ShellInfo.current_directory + folder + @"\";
            }
        }
    }
}
