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

        public HeroProfile(Hero hero)
        {
            ThisHero = hero;
            InitializeComponent();
            DrawProfile(ThisHero);
        }

        private void DrawProfile(Hero hero)
        {
            ProfilePicture.ImageSource = ImageUtils.GetImageSourceFromPath("/" + hero.ProfileImagePath);
            LeftBorder.Source = GetBorderImageSource(hero.Type);
            RightBorder.Source = GetBorderImageSource(hero.Type);
            TopBorder.Source = GetBorderImageSource(hero.Type);
            BottomBorder.Source = GetBorderImageSource(hero.Type);
        }

        private BitmapImage GetBorderImageSource(AppGlobals.Types type)
        {
            var imagePath = ImageUtils.GetProfileBorderImagePathFromType(type);
            return ImageUtils.GetImageSourceFromPath("/" + imagePath);
        }
    }
}
