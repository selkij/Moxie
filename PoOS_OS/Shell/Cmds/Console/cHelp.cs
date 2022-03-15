namespace ProjectOrizonOS.Shell.Cmds.Console
{
    internal class cHelp
    {
        public static void Help()
        {
            Kernel.shell.WriteLine("Cat <file> : output file content");
            Kernel.shell.WriteLine("mkdir <dirname> : creates directory");
            Kernel.shell.WriteLine("rmdir <dirname> : removes directory");
            Kernel.shell.WriteLine("mkfile <file name> : creates file");
            Kernel.shell.WriteLine("rmfile <file name> : removes file");
            Kernel.shell.WriteLine("cd <path> : change working directory");
            Kernel.shell.WriteLine("ls : list files of working directory");
            Kernel.shell.WriteLine("rmfile <file name> : removes file");
            Kernel.shell.WriteLine("cls | clear : clear console");
            Kernel.shell.WriteLine("shutdown | sd : shutdowns computer with AHCI");
            Kernel.shell.WriteLine("reboot | rb : reboots computer");
            Kernel.shell.WriteLine("whoami : displays username");
            Kernel.shell.WriteLine("setKeyboardMap <map> : changes keyboard language");
        }
    }
}
