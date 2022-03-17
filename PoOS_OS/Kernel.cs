using System;
using Sys = Cosmos.System;
using ProjectOrizonOS.Core;
using ProjectOrizonOS.Core.GUI;
using ProjectOrizonOS.Shell;

namespace ProjectOrizonOS
{
    public class Kernel : Sys.Kernel
    {
        //vFS
        public static string current_directory = @"0:\";

        //Instantiate
        public static ShellManager shell = new();
        private Initializer init = new();
        public static GuiManager guiManager = new();
        private static Setup setup = new();
        
        protected override void BeforeRun()
        {
            try
            {
                //init.vFS();
                //init.DHCP();
            }
            catch (Exception ex)
            {
                shell.Log($"Failed Booting ProjectOrizonOS: {ex}", 3);
            }
            
            //setup.StartSetup();
            
            Console.Clear();
            
            guiManager.Initialize();
        }

        protected override void Run()
        {
            guiManager.DrawGUI();
        }
    }
}
