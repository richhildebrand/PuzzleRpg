using System;
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
    public partial class MainPage : PhoneApplicationPage
    {
        PuzzleGrid _puzzleGrid;
        PuzzleGame _puzzleGame;
        Dungeon _activeDungeon;
        DungeonDatabase _dungeonDatabase;

        public MainPage()
        {
            _dungeonDatabase = new DungeonDatabase();

            InitializeComponent();
            PopupUtils.CoverScreen(100);
            Loaded += LoadGraphics;
            MessageBus.Default.Register("EndGame", OnEndGame);
            MessageBus.Default.Register("MonsterDefeated", MonsterDefeated);
        }

        private void OnEndGame(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/DungeonSelection.xaml", UriKind.RelativeOrAbsolute));
        }

        private void MonsterDefeated(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TeamVictory.xaml", UriKind.RelativeOrAbsolute));
        }

        public async void LoadGraphics(object sender, RoutedEventArgs e)
        {
            var monsterGrid = new MonsterGrid(MonsterGrid, _activeDungeon);

            var activeTeam = new Team();
            HeroGrid.AddHeroes(activeTeam);
            PlayerHealth.HealthPercentage.ColumnDefinitions[0].MaxWidth = PlayerHealth.HealthPercentage.ActualWidth;

            _puzzleGrid = new PuzzleGrid(PuzzleGrid, AppGlobals.PuzzleGridRowCount, AppGlobals.PuzzleGridColumnCount);

            _puzzleGame = new PuzzleGame(_puzzleGrid, PlayerHealth, activeTeam, monsterGrid);
            _puzzleGame.StartGame();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string queryStringParam = "";
            if (NavigationContext.QueryString.TryGetValue("dungeonToEnter", out queryStringParam))
            {
                var idOfDungeon = Convert.ToInt32(queryStringParam);
                _activeDungeon = _dungeonDatabase.AllDungeons.Single(d => d.Id == idOfDungeon);
            }
        }
    }
}