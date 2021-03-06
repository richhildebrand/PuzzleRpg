﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Screens
{
    public class TeamDeathScreen
    {
        private Popup _modal;
        private Grid _modalContent;
        private String _message;

        public TeamDeathScreen(String message)
        {
            _modal = new Popup();
            _message = message;

            var popupWidth = Application.Current.Host.Content.ActualWidth * .8;
            var popupHeight = Application.Current.Host.Content.ActualHeight * .75;
            _modalContent = CreateContent(popupWidth, popupHeight);
            _modal.Child = _modalContent;
        }

        public void Show()
        {
            PopupUtils.CoverScreen(65);
            _modal.HorizontalOffset = (Application.Current.Host.Content.ActualWidth - _modalContent.Width) / 2;
            _modal.VerticalOffset = (Application.Current.Host.Content.ActualHeight - _modalContent.Height) / 2;
            _modal.IsOpen = true;
        }

        private Grid CreateContent(double width, double height)
        {
            var grid = InitGrid(1);
            grid = StyleGrid(grid);
            grid = AddControl(0, grid);
            grid = SizeGrid(width, height, grid); //must come last
            return grid;
        }

        private Grid AddControl(int row, Grid grid)
        {
            var navBar = new HeroProfileInHeroBox();

            grid.Children.Add(navBar);
            navBar.SetValue(Grid.RowProperty, row);
            return grid;
        }
  
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            _modal.IsOpen = false;
            PopupUtils.UncoverScreen();
        }
  
        private Grid SizeGrid(double width, double height, Grid grid)
        {
            grid.Width = width;
            grid.Height = height;
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
            grid.Background = new SolidColorBrush(Colors.Red);
            return grid;
        }

    }
}
