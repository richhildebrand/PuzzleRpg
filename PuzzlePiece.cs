﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public class PuzzlePiece
    {
        private readonly TranslateTransform _dragTranslation;

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
        }

        public void SetPosition(int row, int column) 
        {
            Location = new Node(row, column);
            _dragTranslation.Y = row * 83;// Element.ActualHeight;
            _dragTranslation.X = column * 80;//Element.ActualWidth;
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

        private Image AddTouchEvents(Image orb)
        {
            //BehaviorCollection behaviors = Interaction.GetBehaviors(orb);
            //var mouseDragBehavior = new MouseDragElementBehavior();
            //mouseDragBehavior.ConstrainToParentBounds = true;
            //behaviors.Add(mouseDragBehavior);

            orb.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(MovingPuzzlePiece);
            orb.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(DropPuzzlePiece);
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