using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PuzzleRpg.Utils
{
    public static class PositionCalculator
    {
        public static int GetCurrentRowAfterMove(Image image, int currentRow)
        {
            var nearestRow = GetNearestRow(image);
            if (nearestRow != currentRow)
            {
                //TODO: Move orb from new row to old row
                //Debug.WriteLine("nearestRow: " + nearestRow + " currentRow: " + currentRow);
                currentRow = Convert.ToInt32(nearestRow);
            }
            return currentRow;
        }

        public static int GetCurrentColumnAfterMove(double movedTo, double columnSize, int currentColumn)
        {
            var nearestColumn = Math.Round(movedTo / columnSize);
            if (nearestColumn != currentColumn)
            {
                //TODO: Move orb from new column to old column
                //Debug.WriteLine("nearestColumn: " + nearestColumn + " currentColumn: " + currentColumn);
                currentColumn = Convert.ToInt32(nearestColumn);
            }
            return currentColumn;
        }

        public static double NearestRowEdge(Image image)
        {
            var rowSize = image.ActualHeight;
            var nearestRow = GetNearestRow(image);
            var nearestEdge = nearestRow * rowSize;
            return nearestEdge;
        }

        public static double NearestColumnEdge(double droppedAt, double itemWidth)
        {
            var columnSize = itemWidth;
            var nearestColumn = Math.Round(droppedAt / columnSize);
            var nearestEdge = nearestColumn * columnSize;

            return nearestEdge;
        }

        private static int GetNearestRow(Image image)
        {
            var grid = image.Parent as Grid;
            var rowSize = image.ActualHeight;

            var pointInRelationToGrid = grid.TransformToVisual(image).Transform(new Point(0, 0));
            var nearestRow = Math.Abs(Math.Round(pointInRelationToGrid.Y / rowSize));
            return Convert.ToInt32(nearestRow);
        }
    }
}
