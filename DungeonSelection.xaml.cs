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
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?duneonToEnter=" + dungeonToEnter,
                                            UriKind.RelativeOrAbsolute));
        }
    }
}