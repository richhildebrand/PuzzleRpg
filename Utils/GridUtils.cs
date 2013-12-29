using System;
using System.Linq;
using System.Windows.Controls;

namespace PuzzleRpg.Utils
{
    public static class GridUtils
    {
        public static Grid AddRowsToGrid(Grid grid, int rowsToAdd)
        {
            for (int i = 0; i < rowsToAdd; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            return grid;
        }

        public static Grid AddColumnsToGrid(Grid grid, int columnsToAdd)
        {
            for (int i = 0; i < columnsToAdd; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            return grid;
        }
    }
}
