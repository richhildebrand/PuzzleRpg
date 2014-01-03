using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Microsoft.Expression.Interactivity.Layout;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public class PuzzlePiece
    {
        public readonly TranslateTransform _dragTranslation;

        public Node Location { get; set; }
        public Image Element { get; set; }
        public bool Matched { get; set; }
        public string Type { get; set; }

        public PuzzlePiece(int row, int column)
        {
            Matched = false;
            _dragTranslation = new TranslateTransform();

            Element = new Image();
            Element = StyleOrb(Element);
            Element = AddTouchEvents(Element);
            SetPosition(row, column);
            _dragTranslation.Y -= AppGlobals.PuzzleGridActualHeight;

            BehaviorCollection behaviors = Interaction.GetBehaviors(Element);
            var mouseDragBehavior = new MouseDragElementBehavior();
            mouseDragBehavior.ConstrainToParentBounds = true;
            behaviors.Add(mouseDragBehavior);
        }

        public void SetPosition(int row, int column) 
        {
            Location = new Node(row, column);
            var gridHeight = AppGlobals.PuzzleGridActualHeight;
            var rowSize = gridHeight / AppGlobals.PuzzleGridRowCount;
            _dragTranslation.Y = row * rowSize;

            var screenWidth = Application.Current.Host.Content.ActualWidth;
            var columnSize = screenWidth / AppGlobals.PuzzleGridColumnCount;
            _dragTranslation.X = column * columnSize;
        }

        private Image StyleOrb(Image orb)
        {

            orb = ImageUtils.GetImageFromPath(GetOrbType());
            orb.Stretch = System.Windows.Media.Stretch.Fill;
            return orb;
        }

        private string GetOrbType()
        {
            string orbType;
            var randomNumber = MathUtils.GetRandomInteger(0, 4);
            switch (randomNumber)
            {
                case 1: orbType = "Assets/Orbs/WaterOrb.png"; Type = "Water"; break;
                case 2: orbType = "Assets/Orbs/FireOrb.png"; Type = "Fire"; break;
                case 3: orbType = "Assets/Orbs/HealOrb.png"; Type = "Heal"; break;
                default: orbType = "Assets/Orbs/WoodOrb.png"; Type = "Wood"; break;
            }
            return orbType;
        }

        public Image AddTouchEvents(Image orb)
        {
            orb.ManipulationDelta -= MovingPuzzlePiece;
            orb.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(MovingPuzzlePiece);

            orb.ManipulationCompleted -= DropPuzzlePiece;
            orb.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(DropPuzzlePiece);

            orb.RenderTransform = null;
            orb.RenderTransform = this._dragTranslation;

            return orb;
        }
  
        private void DropPuzzlePiece(object sender, ManipulationCompletedEventArgs e)
        {
            var image = sender as Image;
            Canvas.SetZIndex(image, 0);
            _dragTranslation.Y += PositionCalculator.NearestRowEdge(image) - _dragTranslation.Y;
            _dragTranslation.X += PositionCalculator.NearestColumnEdge(_dragTranslation.X, image.ActualWidth) - _dragTranslation.X;
            MessageBus.Default.Notify("RemoveMatchingOrbs", this, new NotificationEventArgs());
        }

        private void MovingPuzzlePiece(object sender, ManipulationDeltaEventArgs e)
        {
            _dragTranslation.X += e.DeltaManipulation.Translation.X;
            _dragTranslation.Y += e.DeltaManipulation.Translation.Y;

            var image = sender as Image;
            Canvas.SetZIndex(image, 1);
            PositionCalculator.SetCurrentColumnAfterMove(_dragTranslation.X, image.ActualWidth, Location);
            PositionCalculator.SetCurrentRowAfterMove(image, Location);
        }
    }
}