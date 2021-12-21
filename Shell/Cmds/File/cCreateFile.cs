using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    class cCreateFile
    {
        private static ShellManager shell = new ShellManager();

        public static void CreateFile()
        {
            try
            {
                VFSManager.CreateFile(@"0:\hi.txt");
                shell.WriteLine("Done!");

            } catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
            

    }
}
