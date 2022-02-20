namespace ProjectOrizonOS.Shell
{
    internal class ShellInfo
    {
        //Primary version Second version Revision BuildNumber
        public static string version = "1.2.0.0";
        public static string langSelected = "en_US";
        public static bool firstRunning = true;
        public static string user;
        public static string machineName;
        public static int shellMode = 1; // 0 = VGA text mode | 1 = Graphic mode
    }
}