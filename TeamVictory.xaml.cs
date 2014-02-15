using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class TeamVictory : PhoneApplicationPage
    {
        public TeamVictory()
        {
            InitializeComponent();

            var dungeonResults = AppGlobals.ActiveDungeonScore;
            this.DataContext = dungeonResults;

            if (dungeonResults.PlayerWins)
            {
                UnlockNextDungeon();
            }
        }

        private void UnlockNextDungeon()
        {
            var defeatedDungeon = AppGlobals.ActiveDungeonScore.ActiveDungeon;
            var dungeonToUnlock = defeatedDungeon.Unlocks;
            var dungeonRepository = new DungeonRepository();
            dungeonRepository.UnlockDungon(dungeonToUnlock);
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