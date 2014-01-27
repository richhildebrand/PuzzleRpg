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
        bool justGotCalled = false;

        public MainPage()
        {
            InitializeComponent();
            PopupUtils.CoverScreen(100);
            Loaded += LoadGraphics;
            MessageBus.Default.Register("EndGame", OnEndGame);
            MessageBus.Default.Register("MonsterDefeated", MonsterDefeated);
        }

        private void OnEndGame(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroBox.xaml", UriKind.RelativeOrAbsolute));
            //Eric: So I have an easy way to view my screen while I work on it :D
            //this.NavigationService.Navigate(new Uri("/TeamVictory.xaml", UriKind.RelativeOrAbsolute));
        }

        private void MonsterDefeated(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TeamVictory.xaml", UriKind.RelativeOrAbsolute));
        }

        public void LoadGraphics(object sender, RoutedEventArgs e)
        {
            if (!justGotCalled)
            {
                var ddb = new DungeonDatabase();
                var activeDungeon = ddb.AllDungeons[0];
                var monsterGrid = new MonsterGrid(MonsterGrid, activeDungeon);

                var activeTeam = new Team();
                HeroGrid.AddHeroes(activeTeam);

                _puzzleGrid = new PuzzleGrid(PuzzleGrid, AppGlobals.PuzzleGridRowCount, AppGlobals.PuzzleGridColumnCount);

                _puzzleGame = new PuzzleGame(_puzzleGrid, PlayerHealth, activeTeam, monsterGrid);
                _puzzleGame.StartGame();
            }
        }

        //For now so I don't accidentally leave the game.
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);

            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit?", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_puzzleGrid == null)
            {
                return;
            }
            justGotCalled = true;
        }
    }
}