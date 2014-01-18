using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using PuzzleRpg.Heroes;
using PuzzleRpg.Monsters;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class MainPage : PhoneApplicationPage
    {
        PuzzleGrid _puzzleGrid;
        PuzzleGame _puzzleGame;

        public MainPage()
        {
            InitializeComponent();
            PopupUtils.CoverScreen(100);
            Loaded += LoadGraphics;
            MessageBus.Default.Register("EndGame", OnEndGame);
        }

        private void OnEndGame(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroBox.xaml", UriKind.RelativeOrAbsolute));
        }

        private void LoadGraphics(object sender, RoutedEventArgs e)
        {
            var monsterGrid = new MonsterGrid(MonsterGrid);

            var activeTeam = new Team();
            HeroGrid.AddHeroes(activeTeam);

            _puzzleGrid = new PuzzleGrid(PuzzleGrid, AppGlobals.PuzzleGridRowCount, AppGlobals.PuzzleGridColumnCount);

            _puzzleGame = new PuzzleGame(_puzzleGrid, PlayerHealth, activeTeam, monsterGrid);
            _puzzleGame.StartGame();
        }
    }
}