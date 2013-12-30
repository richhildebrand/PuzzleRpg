using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Microsoft.Expression.Interactivity.Layout;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public class PuzzlePiece
    {
        private readonly TranslateTransform _dragTranslation;
        private Node _location;

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
            _location = new Node(row, column);
        }

        private Image StyleOrb(Image orb)
        {
            orb = ImageUtils.GetImageFromPath("Assets/Orbs/FireOrb.png");
            orb.Stretch = System.Windows.Media.Stretch.Fill;
            return orb;
        }

        private Image AddTouchEvents(Image orb)
        {
            //BehaviorCollection behaviors = Interaction.GetBehaviors(orb);
            //var mouseDragBehavior = new MouseDragElementBehavior();
            //mouseDragBehavior.ConstrainToParentBounds = true;
            //behaviors.Add(mouseDragBehavior);

            orb.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(MovePuzzlePiece);
            orb.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(DropPuzzlePiece);
            orb.RenderTransform = this._dragTranslation;

            return orb;
        }
  
        private void DropPuzzlePiece(object sender, ManipulationCompletedEventArgs e)
        {
            var image = sender as Image;
            _dragTranslation.Y += PositionCalculator.NearestRowEdge(image) - _dragTranslation.Y;
            _dragTranslation.X += PositionCalculator.NearestColumnEdge(_dragTranslation.X, image.ActualWidth) - _dragTranslation.X;
        }

        void MovePuzzlePiece(object sender, ManipulationDeltaEventArgs e)
        {
            _dragTranslation.X += e.DeltaManipulation.Translation.X;
            _dragTranslation.Y += e.DeltaManipulation.Translation.Y;

            var image = sender as Image;
            _location.Column = PositionCalculator.GetCurrentColumnAfterMove(_dragTranslation.X, image.ActualWidth, _location);
            _location.Row = PositionCalculator.GetCurrentRowAfterMove(image, _location);
        }
    }
}