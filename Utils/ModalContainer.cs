using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PuzzleRpg.Utils
{
    public class ModalContainer
    {
        private readonly Popup _modal;
        private readonly Grid _modalContent;

        public ModalContainer(UIElement modalContent)
        {
            _modal = new Popup();
            _modalContent = GetWrapperWith(modalContent);
            var borderWithContent = AddBorderTo(_modalContent);
            _modal.Child = borderWithContent;
        }

        private Border AddBorderTo(Grid contentWrapper)
        {
            var border = new Border();
            border.Child = contentWrapper;
            border.Background = new SolidColorBrush(Colors.LightGray);
            border.BorderThickness = new Thickness(2);
            border.Padding = new Thickness(5);
            border.CornerRadius = new CornerRadius(5);
            return border;
        }

        public void Show()
        {
            PopupUtils.CoverScreen(65);
            _modal.HorizontalOffset = (Application.Current.Host.Content.ActualWidth - _modalContent.Width) / 2;
            _modal.VerticalOffset = (Application.Current.Host.Content.ActualHeight - _modalContent.Height) / 2;
            _modal.IsOpen = true;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            _modal.IsOpen = false;
            PopupUtils.UncoverScreen();
        }

        private Grid GetWrapperWith(UIElement modalContent)
        {
            var grid = InitGrid();
            grid = AddUIElement(grid, modalContent);
            grid = SizeGrid(grid); //must come last
            return grid;
        }

        private Grid AddUIElement(Grid grid, UIElement modalContent)
        {
            grid.Children.Add(modalContent);
            modalContent.SetValue(Grid.RowProperty, 0);
            return grid;
        }

        private Grid SizeGrid(Grid grid)
        {
            grid.Width = Application.Current.Host.Content.ActualWidth * .8;
            grid.Height = Application.Current.Host.Content.ActualHeight * .75;
            return grid;
        }

        private Grid InitGrid()
        {
            Grid grid = new Grid();
            GridUtils.AddRowsToGrid(grid, 1);
            return grid;
        }
    }
}
