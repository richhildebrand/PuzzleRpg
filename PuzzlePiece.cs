using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public class PuzzlePiece
    {
        private readonly TranslateTransform _dragTranslation;
        private int _row;
        private int _column;

        public Image Element { get; set; }

        public PuzzlePiece(int row, int column)
        {
            _dragTranslation = new TranslateTransform();
            SetGridPosition(row, column);

            Element = new Image();
            Element = StyleOrb(Element);
            Element = AddTouchEvents(Element);
        }

        private void SetGridPosition(int row, int column) 
        {
            _row = row;
            _column = column;
        }

        private Image StyleOrb(Image orb)
        {
            orb = ImageUtils.GetImageFromPath("Assets/Orbs/FireOrb.png");
            orb.Stretch = System.Windows.Media.Stretch.Fill;
            return orb;
        }

        private Image AddTouchEvents(Image orb)
        {
            orb.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(Drag_ManipulationDelta);
            orb.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(DropPuzzlePiece);
            orb.RenderTransform = this._dragTranslation;
            //orb.SetValue(MouseDragElementBehavior.ConstrainToParentBoundsProperty, true);
            return orb;
        }
  
        private void DropPuzzlePiece(object sender, ManipulationCompletedEventArgs e)
        {
            var image = sender as Image;
            _dragTranslation.Y += NearestRowEdge(image) - _dragTranslation.Y;
            _dragTranslation.X += NearestColumnEdge(_dragTranslation.X, image.ActualWidth) - _dragTranslation.X;
        }

        private double NearestRowEdge(Image image)
        {
            var rowSize = image.ActualHeight;
            var nearestRow = GetNearestRow(image);
            var nearestEdge = nearestRow * rowSize;
            return nearestEdge;
        }

        private int GetNearestRow(Image image) 
        {
            var grid = image.Parent as Grid;
            var rowSize = image.ActualHeight;

            var pointInRelationToGrid = grid.TransformToVisual(image).Transform(new Point(0, 0));
            var nearestRow = Math.Abs(Math.Round(pointInRelationToGrid.Y / rowSize));
            return Convert.ToInt32(nearestRow);
        }

        private double NearestColumnEdge(double droppedAt, double itemWidth)
        {
            var columnSize = itemWidth;
            var nearestColumn = Math.Round(droppedAt / columnSize);
            var nearestEdge = nearestColumn * columnSize;

            return nearestEdge;
        }

        void Drag_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            _dragTranslation.X += e.DeltaManipulation.Translation.X;
            _dragTranslation.Y += e.DeltaManipulation.Translation.Y;

            var image = sender as Image;
            _column = GetCurrentColumnAfterMove(_dragTranslation.X, image.ActualWidth, _column);
            _row = GetCurrentRowAfterMove(image, _row);
        }

        private int GetCurrentRowAfterMove(Image image, int currentRow)
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

        private int GetCurrentColumnAfterMove(double movedTo, double columnSize, int currentColumn)
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
    }
}
