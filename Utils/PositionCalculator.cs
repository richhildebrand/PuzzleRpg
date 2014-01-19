using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PuzzleRpg.Models;
using SimpleMvvmToolkit;

namespace PuzzleRpg.Utils
{
    public static class PositionCalculator
    {
        public static void SetCurrentRowAfterMove(Image image, Node origin)
        {
            var nearestRow = Convert.ToInt32(GetNearestRow(image));
            if (nearestRow != origin.Row)
            {
                var destination = new Node(nearestRow, origin.Column);
                var orbMove = new OrbMove(origin, destination);
                MessageBus.Default.Notify("SwapOrbs", orbMove, new NotificationEventArgs());
                origin.Row = nearestRow;
            }
        }

        public static void SetCurrentColumnAfterMove(double movedTo, double columnSize, Node origin)
        {
            var nearestColumn = Convert.ToInt32(Math.Round(movedTo / columnSize));
            if (nearestColumn != origin.Column)
            {
                var destination = new Node(origin.Row, nearestColumn);
                var orbMove = new OrbMove(origin, destination);
                MessageBus.Default.Notify("SwapOrbs", orbMove, new NotificationEventArgs());
            }
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
