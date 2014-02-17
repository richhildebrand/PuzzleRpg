using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PuzzleRpg.Animations
{
    public static class MonsterDeathAnimator
    {
        private static TaskCompletionSource<bool> _taskSource;
        private static Storyboard _storyboard;
        private static Image _monsterImage;

        public static Task AnimateDeath(Image monsterImage)
        {
            _monsterImage = monsterImage;
            _taskSource = new TaskCompletionSource<bool>();
            _storyboard = new Storyboard();
            _storyboard.Duration = new Duration(TimeSpan.FromMilliseconds(800));
            _storyboard.Completed += EndAllAnimation;

            AddImageRotation(monsterImage);

            _storyboard.Begin();
            return _taskSource.Task;
        }

        private static void AddImageRotation(Image monsterImage) 
        {
            var opacityAnimation = new DoubleAnimation
            {
                To = 0,
                From = 1,
                Duration = TimeSpan.FromMilliseconds(600)
            };

            Storyboard.SetTarget(opacityAnimation, monsterImage);
            Storyboard.SetTargetProperty(opacityAnimation,
                                         new PropertyPath(Image.OpacityProperty));

            _storyboard.Children.Add(opacityAnimation);
        }

        private static void EndAllAnimation(object sender, EventArgs e)
        {
            _storyboard.Stop();
            _taskSource.SetResult(true);
        }
    }
}
