namespace ProjectOrizonOS.Shell.Cmds.Console
{
    internal class cHelp
    {

        private static ShellManager shell = new();

        public static void Help()
        {
            shell.WriteLine("Cat <file> : output file content");
            shell.WriteLine("mkdir <dirname> : creates directory");
            shell.WriteLine("rmdir <dirname> : removes directory");
            shell.WriteLine("mkfile <file name> : creates file");
            shell.WriteLine("rmfile <file name> : removes file");
            shell.WriteLine("cd <path> : change working directory");
            shell.WriteLine("ls : list files of working directory");
            shell.WriteLine("rmfile <file name> : removes file");
            shell.WriteLine("cls | clear : clear console");
            shell.WriteLine("shutdown | sd : shutdowns computer with AHCI");
            shell.WriteLine("reboot | rb : reboots computer");
            shell.WriteLine("whoami : displays username");
            shell.WriteLine("setKeyboardMap <map> : changes keyboard language");
        }
    }
}
