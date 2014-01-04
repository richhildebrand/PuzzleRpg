using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PuzzleRpg.Heroes
{
    public class CastSpellModal
    {
        private Popup _modal;
        private Grid _modalContent;

        public CastSpellModal()
        {
            _modal = new Popup();

            var popupWidth = Application.Current.Host.Content.ActualWidth * .8;
            var popupHeight = Application.Current.Host.Content.ActualHeight * .75;
            _modalContent = CreateContent(popupWidth, popupHeight, _modal);
            _modal.Child = _modalContent;
        }

        public void Show()
        {
            _modal.HorizontalOffset = (Application.Current.Host.Content.ActualWidth - _modalContent.Width) / 2;
            _modal.VerticalOffset = (Application.Current.Host.Content.ActualHeight - _modalContent.Height) / 2;
            _modal.IsOpen = true;
        }

        private Grid CreateContent(double width, double height, Popup modal)
        {
            Grid grid = new Grid();
            grid.Background = new SolidColorBrush(Colors.Blue);
            modal.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Width = width;
            grid.Height = height;

            return grid;
        }

    }
}
