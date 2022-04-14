using System;

namespace Moxie.Shell.Cmds.File
{
    internal class cListDir
    {

        public static void ListDir()
        {
            try
            {
                string[] filesList = System.IO.Directory.GetFiles(Kernel.CurrentDirectory);
                string[] directoriesList = System.IO.Directory.GetDirectories(Kernel.CurrentDirectory);

                foreach (var entry in directoriesList)
                    Kernel.shell.Write(entry+" ", ConsoleColor.Blue);
                foreach (var entry in filesList)
                    Kernel.shell.Write(entry+" ");
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
