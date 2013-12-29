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
            Element = new Image();
            SetGridPosition(row, column);
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
            _dragTranslation.Y += GetNearestRowEdge(_dragTranslation.Y, image) - _dragTranslation.Y;
            _dragTranslation.X += GetNearestColumnEdge(_dragTranslation.X, image.ActualWidth) - _dragTranslation.X;

            // probably need to keep track of correct grid location
            //image.SetValue(Grid.ColumnProperty, GetDropRow(_yLocation));
            //image.SetValue(Grid.RowProperty, GetDropColumn(_xLocation));
        }

        private double GetNearestRowEdge(double droppedAt, Image image)
        {
            var screenHeight = Application.Current.Host.Content.ActualHeight;
            var rowSize = image.ActualHeight;

            var grid = image.Parent as Grid;
            var pointInRelationToGrid = grid.TransformToVisual(image).Transform(new Point(0, 0));
            var nearestRow = Math.Abs(Math.Round(pointInRelationToGrid.Y / rowSize));
            var distanceToNearestEdge = nearestRow * rowSize;

            return distanceToNearestEdge;
        }

        private double GetNearestColumnEdge(double droppedAt, double itemWidth)
        {
            var columnSize = itemWidth;
            var nearestColumn = Math.Round(droppedAt / columnSize);
            var distanceToNearestEdge = nearestColumn * columnSize;
            return distanceToNearestEdge;
        }

        void Drag_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            //UpdateCurrentColumn(e.DeltaManipulation.Translation.X);
            //UpdateCurrentRow(_dragTranslation.Y += e.DeltaManipulation.Translation.Y);

            _dragTranslation.X += e.DeltaManipulation.Translation.X;
            _dragTranslation.Y += e.DeltaManipulation.Translation.Y;

        }
  
        private void UpdateCurrentRow(double y)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
  
        private void UpdateCurrentColumn(double x)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
