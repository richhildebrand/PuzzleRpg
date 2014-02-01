using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public class AnimateMatch
    {
        private TaskCompletionSource<bool> _taskSource;


        public Task FadeOrbs(List<PuzzlePiece> puzzlePieces)
        {
            _taskSource = new TaskCompletionSource<bool>();

            var storyboard = new Storyboard();

            foreach (var puzzlePiece in puzzlePieces)
            {
                var animation = DoAnimation(puzzlePiece);
                storyboard.Children.Add(animation);
            }

            storyboard.Completed += EndAnimation;
            storyboard.Begin();

            return _taskSource.Task;
        }

        private DoubleAnimation DoAnimation(PuzzlePiece puzzlePiece)
        {
             var opacityAnimation = new DoubleAnimation
            {
                To = 0,
                From = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            Storyboard.SetTarget(opacityAnimation, puzzlePiece.Element);
            Storyboard.SetTargetProperty(opacityAnimation,
                                            new PropertyPath(Image.OpacityProperty));

            return opacityAnimation;
        }

        private void EndAnimation(object sender, EventArgs e)
        {
            _taskSource.SetResult(true);
        }
    }
}
