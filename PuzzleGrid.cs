using System;
using System.Linq;
using System.Windows.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public class PuzzleGrid
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly Grid _grid;

        public PuzzleGrid(Grid puzzleGrid, int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _grid = puzzleGrid;

            _grid = GridUtils.AddRowsToGrid(puzzleGrid, rows);
            _grid = GridUtils.AddColumnsToGrid(puzzleGrid, columns);
            _grid = CreateCheckerBoardOnGrid(puzzleGrid, rows, columns);
        }

        public void AddOrbs()
        {
            var orb = new PuzzlePiece();
            _grid.Children.Add(orb.Element);
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