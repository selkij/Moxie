using System;
using System.Text;
using Sys = Cosmos.System;

namespace SkippleOS.Shell.Cmds.File
{
    internal class cCat
    {
        private static ShellManager shell = new ShellManager();

        public static void Cat()
        {
            shell.Write("File to output: ", ConsoleColor.Gray);
            var input = System.Console.ReadLine();

            if(input != " ")
            {
                try
                {
                    var file = Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\" + input);
                    var file_stream = file.GetFileStream();

                    if (file_stream.CanRead)
                    {
                        byte[] text_to_read = new byte[file_stream.Length];
                        file_stream.Read(text_to_read, 0, (int)file_stream.Length);
                        shell.WriteLine(Encoding.Default.GetString(text_to_read));
                    } else
                    {
                        shell.WriteLine("SkippleOS is needing read permissions for this file.", type: 3);
                    }
                }
                catch (Exception ex)
                {
                    shell.WriteLine(ex.ToString(), type: 3);
                }
            }
        }
    }
}
