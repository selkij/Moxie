using System.Collections.Generic;

namespace Moxie.Shell.Cmds
{
    public class ICommand
    {
        public string[] CommandValues;

        public ICommand(string[] commandvalues)
        {
            CommandValues = commandvalues;
        }

        public virtual void Execute()
        {
        }

        public virtual void Execute(List<string> args)
        {
        }

        public virtual void Help()
        {
            Kernel.shell.WriteLine("No help available.", type: 3);
        }

        public bool ContainsCommand(string command)
        {
            foreach (var commandvalue in CommandValues)
                if (commandvalue == command)
                    return true;
            return false;
        }
    }
}