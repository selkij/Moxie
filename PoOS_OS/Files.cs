using IL2CPU.API.Attribs;

namespace ProjectOrizonOS
{
    public class Files
    {
        [ManifestResourceStream(ResourceName = "ProjectOrizonOS.Resources.wallpaper1920-1080.bmp")]
        public static byte[] WallpaperHD;

        [ManifestResourceStream(ResourceName = "ProjectOrizonOS.Resources.wallpaper1024-768.bmp")]
        public static byte[] Wallpaper1024_768;

        [ManifestResourceStream(ResourceName = "ProjectOrizonOS.Resources.cursor.bmp")]
        public static byte[] Cursor;
        
        [ManifestResourceStream(ResourceName = "ProjectOrizonOS.Resources.PoOS-Logo-White_200x200.bmp")]
        public static byte[] LogoWhite200_200;
        
        [ManifestResourceStream(ResourceName = "ProjectOrizonOS.Resources.PoOS-Logo_30x30.bmp")]
        public static byte[] Logo30_30;
    }
}
