using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class HeroProfileInHeroBox : UserControl
    {
        public HeroProfileInHeroBox()
        {
            InitializeComponent();
        }

        public void Draw(HeroViewModel hero)
        {
            BorderBackground.ImageSource = ImageUtils.GetImageSourceFromPath(hero.BorderImageSource);
            HeroId.Tag = hero.Id;
            ProfileImage.Source = ImageUtils.GetImageSourceFromPath(hero.ProfileImageSource);
            OrbImage.Source = ImageUtils.GetImageSourceFromPath(hero.OrbImageSource);
        }

        public void OnSelectHero(object sender, GestureEventArgs e)
        {
            var target = sender as Grid;
            if (target.Tag != null)
            {
                var targetId = target.Tag.ToString();
                MessageBus.Default.Notify("ShowHeroDetails", new Object(), new NotificationEventArgs(targetId));
            }
        }
    }
}
