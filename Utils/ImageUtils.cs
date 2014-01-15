using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PuzzleRpg.Utils
{
    public static class ImageUtils
    {
        public static Image GetImageFromPath(String path)
        {
            

            Image image = new Image();
            image.Source = GetImageSourceFromPath(path);
            return image;
        }

        public static BitmapImage GetImageSourceFromPath(String path)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(path, UriKind.Relative);
            return bitmapImage;
        }

        public static String GetOrbImageFromType(AppGlobals.Types orbType) 
        {
            var type = orbType.ToString();
            return "Assets/Orbs/" + type + "Orb.png";
        }
    }
}
