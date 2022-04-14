using System;

namespace Moxie.Shell.Cmds.File
{
    class cCreateFile
    {

        public static void CreateFile(string file)
        {
            try
            {
                System.IO.File.Create(Kernel.CurrentDirectory + @"\" + file);
                Kernel.shell.WriteLine("Created file " + file);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
