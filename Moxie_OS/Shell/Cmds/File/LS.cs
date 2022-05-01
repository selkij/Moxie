using System;
using System.Collections.Generic;
using System.IO;

namespace Moxie.Shell.Cmds.File
{
    internal class ListDir : ICommand
    {
        public ListDir(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute()
        {
            try
            {
                var filesList = Directory.GetFiles(Kernel.CurrentDirectory);
                var directoriesList = Directory.GetDirectories(Kernel.CurrentDirectory);

                foreach (var entry in directoriesList)
                    Kernel.shell.Write(entry + " ", ConsoleColor.Blue);
                foreach (var entry in filesList)
                    Kernel.shell.Write(entry + " ");
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }

        public override void Execute(List<string> args)
        {
            try
            {
                var filesList = Directory.GetFiles(args[0]);
                var directoriesList = Directory.GetDirectories(args[0]);

                foreach (var entry in directoriesList)
                    Kernel.shell.Write(entry + " ", ConsoleColor.Blue);
                foreach (var entry in filesList)
                    Kernel.shell.Write(entry + " ");
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }

        public override void Help()
        {
            Kernel.shell.WriteLine("ls <path> - show entries on path");
        }
    }
}