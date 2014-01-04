using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PuzzleRpg.Utils
{
    public static class PopupUtils
    {
        private static Popup _screenCover;
        private static Grid _screenCoverContent;

        static PopupUtils()
        {
            _screenCover = new Popup();

            _screenCoverContent = new Grid();
            _screenCoverContent.Width = Application.Current.Host.Content.ActualWidth;
            _screenCoverContent.Height = Application.Current.Host.Content.ActualHeight;
            _screenCoverContent.Background = new SolidColorBrush(Colors.Black);

            _screenCover.Child = _screenCoverContent;
        }

        public static void CoverScreen(double percentOpacity) 
        {
            _screenCoverContent.Opacity = percentOpacity / 100;
            _screenCover.IsOpen = true;
        }

        public static void UncoverScreen()
        {
            _screenCover.IsOpen = false;
        }
    }
}
