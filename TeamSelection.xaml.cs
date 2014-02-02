using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class TeamSelection : PhoneApplicationPage
    {
        private HeroRepository _heroRepository;
        private TeamRepository _teamRepository;
        private string _teamMemberToSwap;
        private Team _activeTeam;

        public TeamSelection()
        {
            _heroRepository = new HeroRepository();
            _teamRepository = new TeamRepository();
            _activeTeam = new Team();

            InitializeComponent();
            MessageBus.Default.Register("ShowHeroDetails", OnShowHeroDetails);
            MessageBus.Default.Register("FirstNavigationItem", FirstNavItem);
            MessageBus.Default.Register("SecondNavigationItem_Tap", SecondNavItem);
            MessageBus.Default.Register("ThirdNavigationItem", ThirdNavItem);
            MessageBus.Default.Register("FourthNavigationItem", FourthNavItem);
            MessageBus.Default.Register("FifthNavigationItem", FifthNavItem);

            ShowTeam();
            LoadTeamStats();
            LoadAvailableHeroes();
        }

        public void OnAddHeroToTeam(object sender, GestureEventArgs e)
        {
            var heroToAdd = GetHeroId(sender);
            _activeTeam.RemoveHeroFromTeam(_teamMemberToSwap);
            AddHeroToTeam(heroToAdd);

            LoadTeamStats();
            AvailableHeroes.Visibility = Visibility.Collapsed;
            TeamStats.Visibility = Visibility.Visible;
            LoadAvailableHeroes();
        }

        public void OnBeginSelectingDifferentHero(object sender, GestureEventArgs e)
        {
            AvailableHeroes.Visibility = Visibility.Visible;
            TeamStats.Visibility = Visibility.Collapsed;
            _teamMemberToSwap = GetHeroId(sender);
        }

        private void AddHeroToTeam(string idOfHeroToAdd)
        {
            _activeTeam.AddTeamMember(idOfHeroToAdd);
            ShowTeam();
        }

        private string GetHeroId(object sender) {
            var selectedProfile = sender as HeroProfileInHeroBox;
            var selectedHero = selectedProfile.HeroId;

            if (selectedHero.Tag == null)
            {
                return null;
            }
            var heroId = selectedHero.Tag;
            return heroId.ToString();
        }

        private void ShowTeam()
        {
            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                var teamMember = _activeTeam.TeamMembers.SingleOrDefault(tm => tm.Slot == i);
                var hero = (teamMember != null) ? teamMember.ThisHero : null;
                var heroVM = HeroToViewModelMapper.GetHeroViewModel(hero);

                var heroProfile = Team.Children[i] as HeroProfileInHeroBox;
                heroProfile.Draw(heroVM);
                heroProfile.Tap -= OnBeginSelectingDifferentHero;
                heroProfile.Tap += OnBeginSelectingDifferentHero;
            }
        }

        private void LoadAvailableHeroes()
        {
            var heroProfileSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(AppGlobals.MaxHeroesOnATeam);
            AvailableHeroes.GridCellSize = heroProfileSize;
            AvailableHeroes.Visibility = Visibility.Collapsed;

            var availableHeroes = _heroRepository.GetHeroesOwnedByPlayer();
            var heroesInUse = _activeTeam.TeamMembers.Select(tm => tm.ThisHero).ToList();

            availableHeroes = availableHeroes.Except(heroesInUse).ToList();
            AvailableHeroes.ItemsSource = HeroToViewModelMapper.GetHeroViewModels(availableHeroes);
        }

        private void LoadTeamStats()
        {
            TeamStats.Draw(_activeTeam);
        }

        private void OnShowHeroDetails(object sender, NotificationEventArgs e)
        {
            var id = e.Message;
            this.NavigationService.Navigate(new Uri("/HeroDetails.xaml?playerOwnedHeroId=" + id,
                                            UriKind.RelativeOrAbsolute));
        }

        private void FirstNavItem(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroBox.xaml",
                                            UriKind.RelativeOrAbsolute));
        }

        private void SecondNavItem(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroDetails.xaml",
                                            UriKind.RelativeOrAbsolute));
        }

        private void ThirdNavItem(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml",
                                            UriKind.RelativeOrAbsolute));
        }

        private void FourthNavItem(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TeamVictory.xaml",
                                            UriKind.RelativeOrAbsolute));
        }

        private void FifthNavItem(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TeamSelection.xaml",
                                            UriKind.RelativeOrAbsolute));
        }
    }
}