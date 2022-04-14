using System;

namespace Moxie.Shell.Cmds.File
{
    internal class cCat
    {

        public static void Cat(string fileN)
        {
            try
            {
                Kernel.shell.WriteLine(System.IO.File.ReadAllText(Kernel.CurrentDirectory + fileN));
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
