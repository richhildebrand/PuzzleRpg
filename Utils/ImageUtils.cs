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
            if (path == null)
            {
                return image;
            }

            image.Source = GetImageSourceFromPath(path);
            return image;
        }

        public static BitmapImage GetImageSourceFromPath(String path)
        {
            BitmapImage bitmapImage = new BitmapImage();

            if (path == null)
            {
                return bitmapImage;
            }

            bitmapImage.UriSource = new Uri(path, UriKind.Relative);
            return bitmapImage;
        }

        public static String GetOrbImagePathFromType(AppGlobals.Types orbType) 
        {
            if (orbType == null)
            {
                return "";
            }

            var type = orbType.ToString();
            return "Assets/Orbs/" + type + "Orb.png";
        }

        public static String GetProfileBorderImagePathFromType(AppGlobals.Types profileType)
        {
            if (profileType == null)
            {
                return "";
            }

            var type = profileType.ToString();
            return "Assets/ProfileBorders/" + type + ".png";
        }
    }
}
