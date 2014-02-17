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

            SetStoryBoardDuration();

            _storyboard.Completed += EndAllAnimation;
            FadeImage(monsterImage);

            _storyboard.Begin();
            return _taskSource.Task;
        }

        private static void SetStoryBoardDuration() {
            var totalAnimationTime = AppSettings.MonsterDeathFadeTimeInMilliseconds + AppSettings.MonsterDeathTimeInvisibleInMilliseconds;
            _storyboard.Duration = new Duration(TimeSpan.FromMilliseconds(totalAnimationTime));
        }

        private static void FadeImage(Image monsterImage) 
        {
            var opacityAnimation = new DoubleAnimation
            {
                To = 0,
                From = 1,
                Duration = TimeSpan.FromMilliseconds(AppSettings.MonsterDeathFadeTimeInMilliseconds)
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
