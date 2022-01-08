using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    class cCreateFile
    {
        private static ShellManager shell = new();

        public static void CreateFile(string file)
        {
            try
            {
                VFSManager.CreateFile(Kernel.current_directory + @"\" + file);
                shell.WriteLine("Created file " + file);
            } catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
