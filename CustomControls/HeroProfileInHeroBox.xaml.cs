using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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

        public void OnSelectHero(object sender, GestureEventArgs e)
        {
            var target = sender as Grid;
            var targetId = target.Tag.ToString();
            MessageBus.Default.Notify("ShowHeroDetails", new Object(), new NotificationEventArgs(targetId));
        }
    }
}
