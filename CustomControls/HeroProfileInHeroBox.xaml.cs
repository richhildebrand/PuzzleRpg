using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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
            MessageBus.Default.Notify("ShowHeroDetails", new Object(), new NotificationEventArgs());
        }
    }
}
