﻿using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Logic;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class PlayDungeon: PhoneApplicationPage
    {
        PuzzleGrid _puzzleGrid;
        PuzzleGame _puzzleGame;
        Dungeon _activeDungeon;
        DungeonDatabase _dungeonDatabase;
        TeamRepository _teamRepository;

        public PlayDungeon()
        {
            _dungeonDatabase = new DungeonDatabase();
            _teamRepository = new TeamRepository();

            InitializeComponent();
            PopupUtils.CoverScreen(100);
            Loaded -= LoadGraphics;
            Loaded += LoadGraphics;
        }

        private void OnEndGame(object sender, NotificationEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DungeonSummary.xaml", UriKind.RelativeOrAbsolute));
        }

        public async void LoadGraphics(object sender, RoutedEventArgs e)
        {
            var monsterGrid = new MonsterGrid(MonsterGrid, _activeDungeon);

            var teamFromDatabase = _teamRepository.GetTeam();
            var activeTeam = new Team(teamFromDatabase);
            HeroGrid.AddHeroes(activeTeam);
            PlayerHealth.HealthPercentage.ColumnDefinitions[0].MaxWidth = PlayerHealth.HealthPercentage.ActualWidth;

            _puzzleGrid = new PuzzleGrid(PuzzleGrid, AppGlobals.PuzzleGridRowCount, AppGlobals.PuzzleGridColumnCount);

            _puzzleGame = new PuzzleGame(_puzzleGrid, PlayerHealth, activeTeam, monsterGrid);
            _puzzleGame.StartGame();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MessageBus.Default.Register("EndGame", OnEndGame);

            string queryStringParam = "";
            if (NavigationContext.QueryString.TryGetValue("dungeonToEnter", out queryStringParam))
            {
                var idOfDungeon = Convert.ToInt32(queryStringParam);
                _activeDungeon = _dungeonDatabase.AllDungeons.Single(d => d.Id == idOfDungeon);
                AppGlobals.ActiveDungeonScore = new DungeonScore(_activeDungeon);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _puzzleGrid.Unregister();
            _puzzleGame.Unregister();
            MessageBus.Default.Unregister("EndGame", OnEndGame);
            NavigationService.RemoveBackEntry();
        }
    }
}