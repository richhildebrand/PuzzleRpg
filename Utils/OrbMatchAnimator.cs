using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public class OrbMatchAnimator
    {
        private TaskCompletionSource<bool> _taskSource;
        private readonly int _matchCount;
        private Popup _textModal;

        public OrbMatchAnimator(int matchCount)
        {
            _matchCount = matchCount;
        }

        public Task FadeOrbs(List<PuzzlePiece> puzzlePieces)
        {
            _taskSource = new TaskCompletionSource<bool>();

            var storyboard = new Storyboard();

            AddMatchText(puzzlePieces[0]);

            foreach (var puzzlePiece in puzzlePieces)
            {
                var animation = GetAnimation(puzzlePiece);
                storyboard.Children.Add(animation);
            }

            storyboard.Completed += EndAnimation;
            storyboard.Begin();

            return _taskSource.Task;
        }
  
        private void AddMatchText(PuzzlePiece puzzlePiece)
        {
            var orbImage  = puzzlePiece.Element;
            var grid = orbImage.Parent as Grid;
            var extraHeighPadding = Application.Current.Host.Content.ActualHeight - grid.ActualHeight;

            var modalPosition = new TranslateTransform();
            modalPosition.X = puzzlePiece._dragTranslation.X;
            modalPosition.Y = puzzlePiece._dragTranslation.Y + extraHeighPadding;

            _textModal = new Popup();

            var textModalContent = new TextBlock();
            textModalContent.Height = orbImage.ActualHeight;
            textModalContent.Width = orbImage.ActualWidth;
            textModalContent.Text = "" + _matchCount;
            textModalContent.HorizontalAlignment = HorizontalAlignment.Center;
            textModalContent.VerticalAlignment = VerticalAlignment.Center;
            textModalContent.FontSize = 50;
            textModalContent.FontWeight = FontWeights.Bold;
            textModalContent.Foreground = new SolidColorBrush(Colors.White);

            _textModal.Child = textModalContent;
            _textModal.RenderTransform = modalPosition;
            _textModal.IsOpen = true;
        }

        private DoubleAnimation GetAnimation(PuzzlePiece puzzlePiece)
        {
             var opacityAnimation = new DoubleAnimation
            {
                To = 0,
                From = 1,
                Duration = TimeSpan.FromMilliseconds(400)
            };

            Storyboard.SetTarget(opacityAnimation, puzzlePiece.Element);
            Storyboard.SetTargetProperty(opacityAnimation,
                                            new PropertyPath(Image.OpacityProperty));

            return opacityAnimation;
        }

        private void EndAnimation(object sender, EventArgs e)
        {
            _textModal.IsOpen = false;
            _taskSource.SetResult(true);
        }
    }
}
