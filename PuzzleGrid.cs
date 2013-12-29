using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PuzzleRpg
{
    class PuzzleGrid
    {
        public PuzzleGrid(Grid puzzleGrid, int rows, int columns)
        {
            puzzleGrid = AddRowsToGrid(puzzleGrid, rows);
            puzzleGrid = AddColumnsToGrid(puzzleGrid, columns);
            puzzleGrid = CreateCheckerBoardOnGrid(puzzleGrid, rows, columns);
        }

        private Grid CreateCheckerBoardOnGrid(Grid grid, int rows, int columns)
        {
            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    var image = GetImageForGridLocation(row, column);
                    grid.Children.Add(image);
                    image.SetValue(Grid.ColumnProperty, column);
                    image.SetValue(Grid.RowProperty, row);

                    Grid.SetColumn(image, column);
                    Grid.SetRow(image, row);
                }
            }
            return grid;
        }


        private Image GetImageForGridLocation(int row, int column)
        {
            return (row + column) % 2 == 0 ? GetImageFromPath("Assests/Backgrounds/PrimaryTile.png")
                                           : GetImageFromPath("Assets/Backgrounds/SecondaryTile.png");
        }

        private Image GetImageFromPath(String path)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(path, UriKind.Relative);

            Image image = new Image();
            image.Source = bitmapImage;
            return image;
        }

        private Grid AddRowsToGrid(Grid grid, int rowsToAdd)
        {
            for (int i = 0; i < rowsToAdd; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            return grid;
        }

        private Grid AddColumnsToGrid(Grid grid, int columnsToAdd)
        {
            for (int i = 0; i < columnsToAdd; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            return grid;
        }
    }
}
