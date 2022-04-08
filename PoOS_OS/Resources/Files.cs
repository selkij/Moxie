using IL2CPU.API.Attribs;
using ProjectOrizonOS.Libraries.Graphics;

namespace ProjectOrizonOS.Resources
{
    public class Files
    {
        private const string Base = "ProjectOrizonOS.Resources.";
        
        [ManifestResourceStream(ResourceName = Base + "wallpaper1920-1080.bmp")] private static readonly byte[] Wallpaper19201080;
        [ManifestResourceStream(ResourceName = Base + "wallpaper1024-768.bmp")] private static readonly byte[] Wallpaper1024_768;
        [ManifestResourceStream(ResourceName = Base + "cursor.bmp")] private static readonly byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "PoOS-Logo-White_200x200.bmp")] private static readonly byte[] LogoWhite200_200;
        [ManifestResourceStream(ResourceName = Base + "PoOS-Logo_30x30.bmp")] private static readonly byte[] Logo30_30;
        [ManifestResourceStream(ResourceName = Base + "CloseButton.bmp")] private static readonly byte[] CloseButtonB;
        [ManifestResourceStream(ResourceName = Base + "MaximizeButton.bmp")] private static readonly byte[] MaximizeButtonB;
        [ManifestResourceStream(ResourceName = Base + "MinimizeButton.bmp")] private static readonly byte[] MinimizeButtonB;
        
        public static Bitmap Cursor { get; } = new(CursorB);
        public static Bitmap Logo { get; } = new(LogoWhite200_200);
        public static Bitmap WallpaperHD { get; } = new(Wallpaper19201080);
        public static Bitmap Wallpaper1024X768 { get; } = new(Wallpaper1024_768);
        public static Bitmap LogoWhite300X300 { get; } = new(LogoWhite200_200);
        public static Bitmap Logo30X30 { get; } = new(Logo30_30);
        public static Bitmap CloseButton { get; } = new(CloseButtonB);
        public static Bitmap MaximizeButton { get; } = new(MaximizeButtonB);
        public static Bitmap MinimizeButton { get; } = new(MinimizeButtonB);
    }
}
