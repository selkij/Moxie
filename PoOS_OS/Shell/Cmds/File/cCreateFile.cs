using Cosmos.System.FileSystem.VFS;
using System;

namespace ProjectOrizonOS.Shell.Cmds.File
{
    internal class cCreateFile
    {
        public static void CreateFile(string file)
        {
            try
            {
                VFSManager.CreateFile(Kernel.current_directory + @"\" + file);
                Kernel.shell.WriteLine("Created file " + file);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
