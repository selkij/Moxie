using System;

namespace Moxie.Shell.Cmds.File
{
    internal class cRemoveFile
    {

        public static void RemoveFile(string file)
        {
            try
            {
                System.IO.File.Delete(Kernel.CurrentDirectory + file);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
