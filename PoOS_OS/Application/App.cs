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

            if (type == AppType.TextWindow)
            {
                window = new TextWindow();
                window.DisplayName = name;
            } else if (type == AppType.GraphicsWindow)
            {
                window = new GraphicsWindow();
                window.DisplayName = name;
            }
        }
    }
}