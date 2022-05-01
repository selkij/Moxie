using System;
using System.Collections.Generic;

namespace Moxie.Shell.Cmds.File
{
    internal class CreateFile : ICommand
    {
        public CreateFile(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute(List<string> args)
        {
            try
            {
                System.IO.File.Create(Kernel.CurrentDirectory + @"\" + args[0]);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }

    internal class RemoveFile : ICommand
    {
        public RemoveFile(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute(List<string> args)
        {
            try
            {
                System.IO.File.Delete(Kernel.CurrentDirectory + args[0]);
            }
            catch (Exception ex)
            {
                Kernel.shell.WriteLine(ex.ToString(), type: 3);
            }
        }
    }
}