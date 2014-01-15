using System;
using System.Linq;
using System.Windows.Controls;
using PuzzleRpg.Heroes;
using PuzzleRpg.Utils;

namespace PuzzleRpg.CustomControls
{
    public partial class HeroProfile : UserControl
    {
        public Hero ThisHero { get; set; }

        public HeroProfile(Hero hero)
        {
            ThisHero = hero;
            InitializeComponent();
            DrawProfile(ThisHero);
        }

        private void DrawProfile(Hero hero)
        {
            ProfilePicture.ImageSource = ImageUtils.GetImageSourceFromPath("/" + hero.ProfileImagePath);
        }
    }
}
