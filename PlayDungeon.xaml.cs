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
            Loaded += LoadGraphics;
            MessageBus.Default.Register("EndGame", OnEndGame);
        }

        private void OnEndGame(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TeamVictory.xaml", UriKind.RelativeOrAbsolute));
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

            string queryStringParam = "";
            if (NavigationContext.QueryString.TryGetValue("dungeonToEnter", out queryStringParam))
            {
                var idOfDungeon = Convert.ToInt32(queryStringParam);
                _activeDungeon = _dungeonDatabase.AllDungeons.Single(d => d.Id == idOfDungeon);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NavigationService.RemoveBackEntry();
        }
    }
}