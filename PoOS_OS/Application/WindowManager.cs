using ProjectOrizonOS.Application.Apps;

namespace ProjectOrizonOS.Application
{
    public class WindowManager
    {
        public void DrawWindows()
        {
            foreach (App app in AppManager.Applications)
            {
                if (app.Name == "Debug")
                {
                    Debug debug = new();
                    debug.Draw();
                }
                else
                {
                    app.window.Draw();
                }
            }
        }
    }
}