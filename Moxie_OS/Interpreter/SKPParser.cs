using System;
using System.IO;
using Moxie.Properties;
using Moxie.Shell.Cmds.Console;

namespace Moxie.Interpreter
{
    public class SkpParse
    {
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
                                Info.user = name;
                            }
                        }catch(Exception ex)
                        {
                            Kernel.shell.WriteLine(ex.ToString() + " 1");
                        }
                        
                    } else if(line.StartsWith("keyMap"))
                    {
                        string[] value = line.Split('=');
                        string keyMap = value[1];
                        if (!string.IsNullOrWhiteSpace(keyMap))
                        {
                            cKeyboardMap.SetKeyboardMap(keyMap);
                        }
                    } else if(line.StartsWith("machineName"))
                    {
                        string[] value = line.Split('=');
                        string machineName = value[1];
                        if (!string.IsNullOrWhiteSpace(machineName))
                        {
                            Info.machineName = machineName;
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
