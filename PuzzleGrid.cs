using System;
using System.Linq;
using System.Windows.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public class PuzzleGrid
    {
        public PuzzleGrid(Grid puzzleGrid, int rows, int columns)
        {
            puzzleGrid = GridUtils.AddRowsToGrid(puzzleGrid, rows);
            puzzleGrid = GridUtils.AddColumnsToGrid(puzzleGrid, columns);
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
            return (row + column) % 2 == 0 ? ImageUtils.GetImageFromPath("Assests/Backgrounds/PrimaryTile.png")
                                           : ImageUtils.GetImageFromPath("Assets/Backgrounds/SecondaryTile.png");
        }
    }
}