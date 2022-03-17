using ProjectOrizonOS.Application.Interface;

namespace ProjectOrizonOS.Application
{
    public class App
    {
        public string Name;
        public Window window;
        
        public App(string name, AppType type)
        {
            Name = name;
            window.DisplayName = name;

            if (type == AppType.TextWindow)
            {
                window = new TextWindow();
            }
        }
    }
}