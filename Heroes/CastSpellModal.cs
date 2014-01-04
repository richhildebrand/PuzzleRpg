using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Heroes
{
    public class CastSpellModal
    {
        private Popup _modal;
        private Grid _modalContent;

        public CastSpellModal(Hero heroCastingSpell)
        {
            _modal = new Popup();

            var popupWidth = Application.Current.Host.Content.ActualWidth * .8;
            var popupHeight = Application.Current.Host.Content.ActualHeight * .75;
            _modalContent = CreateContent(popupWidth, popupHeight, heroCastingSpell);
            _modal.Child = _modalContent;
        }

        public void Show()
        {
            _modal.HorizontalOffset = (Application.Current.Host.Content.ActualWidth - _modalContent.Width) / 2;
            _modal.VerticalOffset = (Application.Current.Host.Content.ActualHeight - _modalContent.Height) / 2;
            _modal.IsOpen = true;
        }

        private Grid CreateContent(double width, double height, Hero heroCastingSpell)
        {
            var grid = InitGrid(2);
            grid = StyleGrid(grid);
            grid = PopulateTopRow(0, grid, heroCastingSpell);
            grid = SizeGrid(width, height, grid); //must come last
            return grid;
        }
  
        private Grid SizeGrid(double width, double height, Grid grid)
        {
            grid.Width = width;
            grid.Height = height;
            return grid;
        }
  
        private Grid PopulateTopRow(int row, Grid grid, Hero heroCastingSpell)
        {
           var heroImage = ImageUtils.GetImageFromPath(heroCastingSpell.FullImagePath);
           grid.Children.Add(heroImage);
           heroImage.SetValue(Grid.RowProperty, row);
           heroImage.Stretch = Stretch.Fill;
           return grid;
        }

        private Grid InitGrid(int rows)
        {
            Grid grid = new Grid();
            GridUtils.AddRowsToGrid(grid, rows);
            return grid;
        }

        private Grid StyleGrid(Grid grid)
        {
            grid.Background = new SolidColorBrush(Colors.Blue);
            return grid;
        }

    }
}
