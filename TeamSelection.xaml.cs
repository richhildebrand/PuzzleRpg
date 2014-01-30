using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class TeamSelection : PhoneApplicationPage
    {
        private HeroRepository _heroRepository;

        public TeamSelection()
        {
            _heroRepository = new HeroRepository();

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
            HeroViewModel[] activeHeroProfiles = HeroToViewModelMapper.GetHeroViewModels(activeTeam);

            for (int i = 0; i < activeHeroProfiles.Length; i++)
            {
                var heroProfile = Team.Children[i] as HeroProfileInHeroBox;
                heroProfile.Draw(activeHeroProfiles[i]);
                heroProfile.Tap += OnBeginSelectingDifferentHero;
            }
        }

        private void InitAvailableHeroes()
        {
            var heroProfileSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(AppGlobals.MaxHeroesOnATeam);
            AvailableHeroes.GridCellSize = heroProfileSize;
            AvailableHeroes.Visibility = Visibility.Collapsed;

            //TODO: Filter heroes already on team
            var availableHeroes = _heroRepository.GetHeroesOwnedByPlayer();
            AvailableHeroes.ItemsSource= HeroToViewModelMapper.GetHeroViewModels(availableHeroes);
        }
    }
}