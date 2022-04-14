using System;

namespace Moxie.Shell.Cmds.File
{
    internal class cCD
    {

        public static void CD(string aPath)
        {
            try
            {
                //Add \ if not here
                if (!aPath.EndsWith(@"\") && aPath != "..") aPath = aPath + @"\";
                
                string folder = System.IO.Directory.GetParent($@"{Kernel.CurrentDirectory}{aPath}\").FullName;

                if (!string.IsNullOrWhiteSpace(folder))
                {
                    if (aPath.StartsWith("\"") && aPath.EndsWith("\"")) 
                    {
                        Kernel.shell.WriteLine("Not implemented.",type: 3);
                    } else
                    {
                        Kernel.CurrentDirectory = folder+@"\";
                    }
                }
                else
                {
                    if (aPath != "..")
                    {
                        Kernel.shell.WriteLine("Please enter a valid path.", type: 3);
                    } else
                    {
                        if (Kernel.CurrentDirectory != Kernel.CurrentVolume)
                        {
                            string _folder = System.IO.Directory.GetParent(Kernel.CurrentDirectory).FullName;
                            Kernel.CurrentDirectory = _folder;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}
