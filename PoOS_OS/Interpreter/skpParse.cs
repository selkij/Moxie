using Cosmos.System.FileSystem.VFS;
using ProjectOrizonOS.Shell;
using ProjectOrizonOS.Shell.Cmds.Console;
using System;
using System.IO;

namespace ProjectOrizonOS.Interpreter
{
    class skpParse
    {

        private ShellManager shell = new();

        public void Execute(string file)
        {
            if(file.EndsWith(".skp"))
            {
                string[] lines = File.ReadAllLines(@"0:\cfg.skp");
                foreach (string line in lines)
                {
                    if(line.StartsWith("name"))
                    {
                        try
                        {
                            string[] value = line.Split('=');
                            string name = value[1];
                            if (!String.IsNullOrWhiteSpace(name))
                            {
                                ShellInfo.user = name;
                            }
                        }catch(Exception ex)
                        {
                            shell.WriteLine(ex.ToString());
                        }
                        
                    } else if(line.StartsWith("keyMap"))
                    {
                        string[] value = line.Split('=');
                        string keyMap = value[1];
                        if (!String.IsNullOrWhiteSpace(keyMap))
                        {
                            cKeyboardMap.SetKeyboardMap(keyMap);
                        }
                    } else if(line.StartsWith("machineName"))
                    {
                        string[] value = line.Split('=');
                        string machineName = value[1];
                        if (!String.IsNullOrWhiteSpace(machineName))
                        {
                            ShellInfo.machineName = machineName;
                        }
                    }
                }
            } else
            {
                throw new FileBadExtension(".skp");
            }
        }
    }
}