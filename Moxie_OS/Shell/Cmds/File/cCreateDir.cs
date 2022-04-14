using System;
using System.IO;

namespace Moxie.Shell.Cmds.File
{
    internal class cCreateDir
    {

        public static void CreateDir(string dirName)
        {
            try
            {
                Directory.CreateDirectory(Kernel.CurrentDirectory + dirName);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
