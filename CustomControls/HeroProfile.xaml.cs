using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PuzzleRpg.Heroes;
using PuzzleRpg.Utils;

namespace PuzzleRpg.CustomControls
{
    public partial class HeroProfile : UserControl
    {
        public Hero ThisHero { get; set; }

        public HeroProfile()
        {
            InitializeComponent();
        }

        public void DrawHeroProfile(Hero hero)
        {
            ThisHero = hero;
            DrawProfile(ThisHero);
        }

        private void DrawProfile(Hero hero)
        {
            ProfilePicture.Source = ImageUtils.GetImageSourceFromPath("/" + hero.ProfileImagePath);
            BordedrImage.ImageSource = GetBorderImageSource(hero.Type);
        }

        private BitmapImage GetBorderImageSource(AppGlobals.Types type)
        {
            var imagePath = ImageUtils.GetProfileBorderImagePathFromType(type);
            return ImageUtils.GetImageSourceFromPath("/" + imagePath);
        }
    }
}
