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
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(path, UriKind.Relative);

            Image image = new Image();
            image.Source = bitmapImage;
            return image;
        }
    }
}
