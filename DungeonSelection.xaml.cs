using System;
using System.Linq;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class DungeonSelection : PhoneApplicationPage
    {
        private DungeonRepository _dungeonRepository;

        public DungeonSelection()
        {
            _dungeonRepository = new DungeonRepository();
            InitializeComponent();
        }

        private void LoadUnlockedDungeons() 
        {
            UnlockedDungeons.ItemsSource = null;
            var unlockedDungeons = _dungeonRepository.GetUnlockedDungeons();
            var sortedDungeons = unlockedDungeons.OrderByDescending(d => d.Id);
            UnlockedDungeons.ItemsSource = sortedDungeons.ToList();
        }

        private void OnEnterDungeon(object sender, NotificationEventArgs e)
        {
            var dungeonToEnter = e.Message;
            this.NavigationService.Navigate(new Uri("/PlayDungeon.xaml?dungeonToEnter=" + dungeonToEnter,
                                            UriKind.RelativeOrAbsolute));
        }

        private void OnNavItemTapped(object sender, NotificationEventArgs e)
        {
            var url = e.Message;
            NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MessageBus.Default.Unregister("EnterDungeon", OnEnterDungeon);
            MessageBus.Default.Unregister("NavigateToPage", OnNavItemTapped);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavBar.HighlightPage("/DungeonSelection.xaml");
            MessageBus.Default.Register("NavigateToPage", OnNavItemTapped);
            MessageBus.Default.Register("EnterDungeon", OnEnterDungeon);
            LoadUnlockedDungeons();
        }
    }
}