using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
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
        public bool IsMaxLevel;

        public ExperienceToNextHeroLevel(Hero hero, double expGained)
        {
            _levelCalculator = new HeroLevelCalculator();
            InitializeComponent();

            DrawConditionalElements(hero, expGained);
            DrawHeroProfile(hero);
        }

        private void DrawConditionalElements(Hero hero, double expGained)
        {
            IsMaxLevel = hero.Level == hero.MaxLevel;
            if (IsMaxLevel)
            {
                MaxLevelReached.Text = "Max level of " + hero.MaxLevel + " reached";
                MaxLevelReached.Visibility = Visibility.Visible;
            }
            else
            {
                var nextLevel = hero.Level + 1;
                ExpToNextLevel.Text = "Experience until level " + nextLevel;
                ExpToNextLevel.Visibility = Visibility.Visible;
                DrawExpBar(hero, expGained);
            }
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
            ExpBar.Visibility = Visibility.Visible;
            var expFillColor = new SolidColorBrush(Color.FromArgb(250, 254, 226, 116));
            ExpBar.SetColor(expFillColor);

            var expNeededForThisLevel = GetExpNeeded(hero.Level, hero.BaseExpPerLevel);
            var expNeededForNextLevel = GetExpNeeded(hero.Level + 1, hero.BaseExpPerLevel);
            var expOverThisLevel = Convert.ToInt32(hero.CurrentExp - expNeededForThisLevel);
            var expFormThisLevelToNext = expNeededForNextLevel - expNeededForThisLevel;
            
            ExpBar.SetFillPercentage(expOverThisLevel, expFormThisLevelToNext);
        }

        private int GetExpNeeded(int level, double baseExp)
        {
            var expNeeded = _levelCalculator.GetExpNeededForLevel(level, baseExp);
            return Convert.ToInt32(expNeeded);
        }
    }
}
