using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public class PuzzlePiece
    {
        private readonly TranslateTransform _dragTranslation;

        public Image Element { get; set; }

        public PuzzlePiece()
        {
            _dragTranslation = new TranslateTransform();

            Element = new Image();
            Element = StyleOrb(Element);
            Element = AddTouchEvents(Element);
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
            orb.RenderTransform = this._dragTranslation;
            return orb;
        }

        void Drag_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            _dragTranslation.X += e.DeltaManipulation.Translation.X;
            _dragTranslation.Y += e.DeltaManipulation.Translation.Y;
        }
    }
}
