using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
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
                var dungeonRepository = new DungeonRepository();
                var dungeons = dungeonRepository.GetAllDungeons();
                SetDungeonAsCleared(dungeons);
                UnlockNextDungeon(dungeons);
                dungeonRepository.Save(dungeons);
            }
        }

        private void SetDungeonAsCleared(List<Dungeon> dungeons)
        {
            var defeatedDungeonId = AppGlobals.ActiveDungeonScore.ActiveDungeon.Id;
            var defeatedDungeon = dungeons.Single(d => d.Id == defeatedDungeonId);
            defeatedDungeon.HasBeenDefeated = true;
        }

        private void UnlockNextDungeon(List<Dungeon> dungeons)
        {
            var dungeonToUnlockId = AppGlobals.ActiveDungeonScore.ActiveDungeon.Unlocks;
            var dungeonToUnlock = dungeons.Single(d => d.Id == dungeonToUnlockId);
            dungeonToUnlock.IsAvailable = true;
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