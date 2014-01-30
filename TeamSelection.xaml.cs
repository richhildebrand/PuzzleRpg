using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class TeamSelection : PhoneApplicationPage
    {
        public TeamSelection()
        {
            InitializeComponent();
            ShowTeam();
            InitAvailableHeroes();
        }

        public void OnAddHeroToTeam(object sender, GestureEventArgs e)
        {
            AvailableHeroes.Visibility = Visibility.Collapsed;
            TeamStats.Visibility = Visibility.Visible;
        }

        public void OnBeginSelectingDifferentHero(object sender, GestureEventArgs e)
        {
            AvailableHeroes.Visibility = Visibility.Visible;
            TeamStats.Visibility = Visibility.Collapsed;
        }

        private void ShowTeam()
        {
            Hero[] activeTeam = new Hero[AppGlobals.MaxHeroesOnATeam];
            Team.ItemsSource = HeroToViewModelMapper.GetHeroViewModels(activeTeam);
        }

        private void InitAvailableHeroes()
        {
            AvailableHeroes.Visibility = Visibility.Collapsed;
            var availableHeroes = HeroRepository.GetHeroesOwnedByPlayer();

            //TODO: Filter heroes already on team
            AvailableHeroes.ItemsSource= HeroToViewModelMapper.GetHeroViewModels(availableHeroes);
        }
    }
}