using System;
using System.Linq;
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

            GiveEarnedExpToActiveTeam(dungeonResults);
        }

        public void GiveEarnedExpToActiveTeam(DungeonScore dungeonResults)
        {
            var totalExpGained = dungeonResults.MonstersSlain.Sum(m => m.ExpGivenOnDeath);
            var heroesOnTeam = new HeroesOnActiveTeamGetter().Get();
            var expPerHero = totalExpGained / heroesOnTeam.Count();

            foreach (var hero in heroesOnTeam)
            {
                hero.CurrentExp += expPerHero;
            }

            var heroRepository = new HeroRepository();
            heroRepository.UpdateHeroes(heroesOnTeam);
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