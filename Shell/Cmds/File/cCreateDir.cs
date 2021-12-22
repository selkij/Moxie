using Cosmos.System.FileSystem.VFS;
using System;

namespace SkippleOS.Shell.Cmds.File
{
    internal class cCreateDir
    {

        private static ShellManager shell = new ShellManager();

        public static void CreateDir(string file)
        {
            try
            {
                VFSManager.CreateDirectory(@"0:\" + file);
            }
            catch (Exception ex)
            {
                shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
