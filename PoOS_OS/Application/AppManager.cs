using System.Collections.Generic;

namespace ProjectOrizonOS.Application
{
    static class AppManager
    {
        public static List<App> Applications = new();

        public static void AddNewApp(App app)
        {
            Applications.Add(app);
        }

        public static void RemoveApp(App app)
        {
            Applications.Remove(app);
        }
    }
}