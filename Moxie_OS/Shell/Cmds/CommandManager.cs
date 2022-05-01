using System.Collections.Generic;
using System.Text;
using Moxie.Shell.Cmds.File;

namespace Moxie.Shell.Cmds
{
    public class CommandManager
    {
        public List<ICommand> Commands = new();

        public void ExecuteCommand(string input)
        {
            var args = ParseCommandLine(input);

            var name = args[0];

            if (args.Count > 0) args.RemoveAt(0); //get only arguments

            foreach (var command in Commands)
                if (command.ContainsCommand(name))
                {
                    if (args.Count == 0)
                        command.Execute();
                    else
                        command.Execute(args);
                }
        }

        public void RegisterCommands()
        {
            Commands.Add(new Cat(new[] { "cat" }));
            Commands.Add(new ListDir(new[] { "ls" }));
            Commands.Add(new Shutdown(new[] { "shutdown", "sd" }));
            Commands.Add(new Reboot(new[] { "reboot", "rb" }));
            Commands.Add(new CD(new[] { "cd" }));
            Commands.Add(new CreateFile(new[] { "mkfile" }));
            Commands.Add(new RemoveFile(new[] { "rmfile" }));
            Commands.Add(new CreateDirectory(new[] { "mkdir" }));
            Commands.Add(new RemoveDirectory(new[] { "rmdir" }));
            Commands.Add(new DownloadFile(new[] { "dl" }));
            Commands.Add(new TestFTP(new[] { "ftp" }));
        }

        public static List<string> ParseCommandLine(string cmdLine)
        {
            var args = new List<string>();
            if (string.IsNullOrWhiteSpace(cmdLine)) return args;

            var currentArg = new StringBuilder();
            var inQuotedArg = false;

            for (var i = 0; i < cmdLine.Length; i++)
                if (cmdLine[i] == '"')
                {
                    if (inQuotedArg)
                    {
                        args.Add(currentArg.ToString());
                        currentArg = new StringBuilder();
                        inQuotedArg = false;
                    }
                    else
                    {
                        inQuotedArg = true;
                    }
                }
                else if (cmdLine[i] == ' ')
                {
                    if (inQuotedArg)
                    {
                        currentArg.Append(cmdLine[i]);
                    }
                    else if (currentArg.Length > 0)
                    {
                        args.Add(currentArg.ToString());
                        currentArg = new StringBuilder();
                    }
                }
                else
                {
                    currentArg.Append(cmdLine[i]);
                }

            if (currentArg.Length > 0) args.Add(currentArg.ToString());

            return args;
        }
    }
}