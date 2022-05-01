using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.System.Network.IPv4;

namespace Moxie.Shell.Cmds.File
{
    internal class DownloadFile : ICommand
    {
        public DownloadFile(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute(List<string> args)
        {
            var addressList = args[0].Split('.').ToList();
            
            var destip = new Address(Byte.Parse(addressList[0]), Byte.Parse(addressList[1]), Byte.Parse(addressList[2]), Byte.Parse(addressList[3]));
            var test = Kernel.init.networkManager.TCPconnect(destip, int.Parse(args[1]), int.Parse(args[2]), args[3]);

            for (var i = 0; i < test.Length; i++) Kernel.shell.Write($"{test[i]}");
            var text = System.IO.File.CreateText(@"0:\test.txt");
            text.Write(test);
        }
    }
    
    internal class TestFTP : ICommand
    {
        public TestFTP(string[] commandvalues) : base(commandvalues)
        {
            CommandValues = commandvalues;
        }

        public override void Execute()
        {
            Kernel.init.networkManager.FTPconnect(Kernel.init.fs, @"0:\");
        }
    }
}