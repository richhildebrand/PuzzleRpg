using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class HeroBox : PhoneApplicationPage
    {
        private readonly int HEROES_PER_ROW = 5;
        private HeroRepository _heroRepository;

        public HeroBox()
        {
            InitializeComponent();
            _heroRepository = new HeroRepository();
            LoadPlayerHeroes(HeroGrid);
        }

        private void OnShowHeroDetails(object sender, NotificationEventArgs e)
        {
            var id = e.Message;
            this.NavigationService.Navigate(new Uri("/HeroDetails.xaml?playerOwnedHeroId=" + id,
                                            UriKind.RelativeOrAbsolute));
        }

        private void LoadPlayerHeroes(LongListSelector heroGrid)
        {
            heroGrid.GridCellSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(HEROES_PER_ROW);
            heroGrid.ItemsSource = GetHeroProfiles();
        }

        private List<HeroViewModel> GetHeroProfiles()
        {
            var heroesOwnedByPlayer = _heroRepository.GetHeroesOwnedByPlayer();
            var filledHeroProfiles = HeroToViewModelMapper.GetHeroViewModels(heroesOwnedByPlayer);
            var allHeroProfiles  = AddEmptyProfiles(filledHeroProfiles);
            return allHeroProfiles;
        }

        private List<HeroViewModel> AddEmptyProfiles(List<HeroViewModel> heroProfiles)
        {
            var numberOfHeroSlotsPlayerHasPurchased = 20; //TODO: save in local storage
            var emptySlotsToAdd = numberOfHeroSlotsPlayerHasPurchased - heroProfiles.Count;
            var emptyHeroSlots = EmptyHeroViewModelGetter.GetEmptyHeroViewModel(emptySlotsToAdd);
            return heroProfiles.Concat(emptyHeroSlots).ToList();
        }

        private void OnNavItemTapped(object sender, NotificationEventArgs e)
        {
            var url = e.Message;
            NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavBar.HighlightPage("/HeroOptions.xaml");
            MessageBus.Default.Register("ShowHeroDetails", OnShowHeroDetails);
            MessageBus.Default.Register("NavigateToPage", OnNavItemTapped);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MessageBus.Default.Unregister("NavigateToPage", OnNavItemTapped);
            MessageBus.Default.Unregister("ShowHeroDetails", OnShowHeroDetails);
            NavigationService.RemoveBackEntry();
        }
    }
}