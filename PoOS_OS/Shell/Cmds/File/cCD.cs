using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;

namespace ProjectOrizonOS.Shell.Cmds.File
{
    internal class cCD
    {
        private static ShellManager shell = new();

        public static void CD(string aPath)
        {
            try
            {
                DirectoryEntry folder = VFSManager.GetDirectory($@"{Kernel.current_directory}{aPath}\");
                if (!string.IsNullOrWhiteSpace(folder.mFullPath))
                {
                    if (aPath.StartsWith("\"") && aPath.EndsWith("\"")) 
                    {
                        shell.WriteLine("Not implemented.",type: 3);
                    } else
                    {
                        if(Kernel.current_directory != Kernel.current_volume)
                        {
                            Kernel.current_directory = folder.mFullPath;
                        }
                    }
                }
                else
                {
                    if (aPath != "..")
                    {
                        shell.WriteLine("Please enter a valid path.", type: 3);
                    }
                }

            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
