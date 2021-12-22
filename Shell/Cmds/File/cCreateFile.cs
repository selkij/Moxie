using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    class cCreateFile
    {
        private static ShellManager shell = new ShellManager();

        public static void CreateFile(string file)
        {
            try
            {
                VFSManager.CreateFile(@"0:\" + file);
            } catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
