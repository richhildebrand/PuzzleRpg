using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class DeleteHero : PhoneApplicationPage
    {
        private readonly int HEROES_PER_ROW = 5;
        private HeroRepository _heroRepository;

        public DeleteHero()
        {
            InitializeComponent();
            _heroRepository = new HeroRepository();
            LoadPlayerHeroes(HeroGrid);
        }

        public void LaunchDeleteHeroModal(object sender, GestureEventArgs e)
        {
            var heroToDelete = GetHeroToDelete(sender);

            var deleteHeroConfirmationModal = new Modals.DeleteHeroConfirmation(heroToDelete);
            deleteHeroConfirmationModal.CloseModal += RemoveDeletedHeroFromScreen;
            var deleteHeroModal = new ModalContainer(deleteHeroConfirmationModal);
            deleteHeroModal.Show();
        }

        private void RemoveDeletedHeroFromScreen(object sender, EventArgs e)
        {
            LoadPlayerHeroes(HeroGrid);
        }

        private void LoadPlayerHeroes(LongListSelector heroGrid)
        {
            heroGrid.GridCellSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(HEROES_PER_ROW);
            var heroesOwnedByPlayer = _heroRepository.GetHeroesOwnedByPlayer();
            heroGrid.ItemsSource = HeroToViewModelMapper.GetHeroViewModels(heroesOwnedByPlayer);
        }

        private Hero GetHeroToDelete(object sender)
        {
            var heroProfile = sender as HeroProfileInHeroBox;
            var heroIdAsString = (string)heroProfile.HeroId.Tag;
            var heroId = new Guid(heroIdAsString);
            return _heroRepository.GetHeroesOwnedByPlayer().Single(h => h.Id == heroId);
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
            NavBar.HighlightPage("/HeroOptions.xaml");
            MessageBus.Default.Register("ShowHeroDetails", OnShowHeroDetails);
            MessageBus.Default.Register("NavigateToPage", OnNavItemTapped);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MessageBus.Default.Unregister("NavigateToPage", OnNavItemTapped);
            MessageBus.Default.Unregister("ShowHeroDetails", OnShowHeroDetails);
        }
    }
}