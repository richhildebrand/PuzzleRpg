using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using PuzzleRpg.Logic;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg.CustomControls
{
    public partial class ExperienceToNextHeroLevel : UserControl
    {
        private readonly HeroLevelCalculator _levelCalculator;

        public ExperienceToNextHeroLevel(Hero hero, double expGained)
        {
            _levelCalculator = new HeroLevelCalculator();
            InitializeComponent();
            DrawHeroProfile(hero);
            DrawExpBar(hero, expGained);
        }

        private void DrawHeroProfile(Hero hero)
        {
            //TODO: we always use 5 so it can be internal to this method.
            var profileSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(5);
            var heroViewModel = HeroToViewModelMapper.GetHeroViewModel(hero);
            Profile.Draw(heroViewModel);
            Profile.Height = profileSize.Height;
            Profile.Width = profileSize.Width;
        }

        private void DrawExpBar(Hero hero, double expGained)
        {
            var expFillColor = new SolidColorBrush(Color.FromArgb(250, 254, 226, 116));
            ExpBar.SetColor(expFillColor);

            var expNeededForThisLevel = GetExpNeeded(hero.Level, hero.BaseExpPerLevel);
            var expNeededForNextLevel = GetExpNeeded(hero.Level + 1, hero.BaseExpPerLevel);
            ExpBar.SetFillPercentage(expNeededForThisLevel, expNeededForNextLevel);
        }

        private int GetExpNeeded(int level, double baseExp)
        {
            var expNeeded = _levelCalculator.GetExpNeededForLevel(level, baseExp);
            return Convert.ToInt32(expNeeded);
        }
    }
}
