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
        private int _teamSlotToModify;
        private Team _activeTeam;

        public TeamSelection()
        {
            _heroRepository = new HeroRepository();
            _teamRepository = new TeamRepository();

            var teamFromDatabase = _teamRepository.GetTeam();
            _activeTeam = new Team(teamFromDatabase);

            InitializeComponent();
            MessageBus.Default.Register("ShowHeroDetails", OnShowHeroDetails);
            MessageBus.Default.Register("NavigateToPage", OnNavItemTapped);

            ShowTeam();
            LoadTeamStats();
            LoadAvailableHeroes();
        }

        public void OnAddHeroToTeam(object sender, GestureEventArgs e)
        {
            var selectedProfile = sender as HeroProfileInHeroBox;
            var heroToAdd = selectedProfile.HeroId.Tag as string;

            _activeTeam.RemoveHeroFromTeam(_teamSlotToModify);
            AddHeroToTeam(_teamSlotToModify, heroToAdd);

            _teamRepository.SaveTeam(_activeTeam);

            LoadTeamStats();
            AvailableHeroes.Visibility = Visibility.Collapsed;
            TeamStats.Visibility = Visibility.Visible;
            LoadAvailableHeroes();
        }

        public void OnBeginSelectingDifferentHero(object sender, GestureEventArgs e)
        {
            var selectedProfile = sender as HeroProfileInHeroBox;
            _teamSlotToModify = (int)selectedProfile.TeamSlot;

            AvailableHeroes.Visibility = Visibility.Visible;
            TeamStats.Visibility = Visibility.Collapsed;
        }

        private void AddHeroToTeam(int teamSlotToAddHero, string idOfHeroToAdd)
        {
            if (idOfHeroToAdd != null)
            {
                _activeTeam.AddTeamMember(teamSlotToAddHero, idOfHeroToAdd);
            }
            ShowTeam();
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
                heroProfile.TeamSlot = i;
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
            var availableHeroVMs = HeroToViewModelMapper.GetHeroViewModels(availableHeroes);
            availableHeroVMs.Insert(0, new HeroViewModel());
            AvailableHeroes.ItemsSource = availableHeroVMs;
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

        private void OnNavItemTapped(object sender, NotificationEventArgs e)
        {
            var url = e.Message;
            NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            MessageBus.Default.Notify("CurrentPage", new Object(), new NotificationEventArgs());

        }
    }
}