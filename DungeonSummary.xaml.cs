using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Getters;
using PuzzleRpg.Logic;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class DungeonSummary : PhoneApplicationPage
    {
        public DungeonSummary()
        {
            InitializeComponent();

            var dungeonResults = AppGlobals.ActiveDungeonScore;
            this.DataContext = dungeonResults;

            if (dungeonResults.PlayerWins)
            {
                var dungeonDefeater = new DungeonDefeater(dungeonResults.ActiveDungeon);
                dungeonDefeater.Defeat();
            }

            SaveAndShowExpericeEarnedByActiveTeam(dungeonResults);
        }

        public void SaveAndShowExpericeEarnedByActiveTeam(DungeonScore dungeonResults)
        {
            var totalExpGained = dungeonResults.MonstersSlain.Sum(m => m.ExpGivenOnDeath);
            var heroesOnTeam = new HeroesOnActiveTeamGetter().Get();
            var expPerHero = totalExpGained / heroesOnTeam.Count();

            SaveEarnedExpForActiveTeam(heroesOnTeam, expPerHero);
            DrawHeroes(heroesOnTeam, expPerHero);
        }

        public void SaveEarnedExpForActiveTeam(List<Hero> heroesOnTeam, double expPerHero)
        {
            foreach (var hero in heroesOnTeam)
            {
                hero.CurrentExp += expPerHero;
            }

            var heroRepository = new HeroRepository();
            heroRepository.UpdateHeroes(heroesOnTeam);
        }

        public void DrawHeroes(List<Hero> heroesOnTeam, double expPerHero) {
            var heroViewModels = HeroToViewModelMapper.GetHeroViewModels(heroesOnTeam);

            //TODO: we always use 5 so it can be internal to this method.
            var profileSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(5);
            var row = 0;
            foreach (var hero in heroViewModels)
            {
                var heroProfile = new CustomControls.HeroProfileInHeroBox();
                heroProfile.Draw(hero);
                heroProfile.SetValue(Grid.RowProperty, row);
                heroProfile.SetValue(Grid.ColumnProperty, 0);
                heroProfile.Height = profileSize.Height;
                heroProfile.Width = profileSize.Width;
                ActiveTeamList.Children.Add(heroProfile);
                row += 1;
            }
        }

        public void OnScreenTap(object sender, GestureEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PopupUtils.UncoverScreen();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NavigationService.RemoveBackEntry();
        }
    }
}