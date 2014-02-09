using System;
using System.Linq;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class DungeonSelection : PhoneApplicationPage
    {
        private DungeonDatabase _dungeonDatabase;

        public DungeonSelection()
        {
            _dungeonDatabase = new DungeonDatabase();
            MessageBus.Default.Register("EnterDungeon", OnEnterDungeon);


            InitializeComponent();
            PopupUtils.UncoverScreen(); //just to be safe
            LoadUnlockedDungeons();
        }

        private void LoadUnlockedDungeons()
        {
            var unlockedDungeons = _dungeonDatabase.AllDungeons;
            //TODO: filter unlocked dungeons
            UnlockedDungeons.ItemsSource = unlockedDungeons;
        }

        private void OnEnterDungeon(object sender, NotificationEventArgs e)
        {
            var dungeonToEnter = e.Message;
            this.NavigationService.Navigate(new Uri("/PlayDungeon.xaml?dungeonToEnter=" + dungeonToEnter,
                                            UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MessageBus.Default.Unregister("EnterDungeon", OnEnterDungeon);
            MessageBus.Default.Unregister("NavigateToPage", OnNavItemTapped);
        }

        private void OnNavItemTapped(object sender, NotificationEventArgs e)
        {
            var url = e.Message;
            NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavBar.HighlightPage("/DungeonSelection.xaml");
            MessageBus.Default.Register("NavigateToPage", OnNavItemTapped);
            MessageBus.Default.Notify("CurrentPage", new Object(), new NotificationEventArgs());

        }
    }
}