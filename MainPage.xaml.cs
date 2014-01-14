using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using PuzzleRpg.Heroes;
using PuzzleRpg.Monsters;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class MainPage : PhoneApplicationPage
    {
        PuzzleGrid _puzzleGrid;
        PuzzleGameEvents _puzzleGame;

        public MainPage()
        {
            InitializeComponent();
            PopupUtils.CoverScreen(100); //lol loading mask...
            Loaded += LoadGraphics;
        }

        private void LoadGraphics(object sender, RoutedEventArgs e)
        {
            InitMonsterGrid();
            InitHeroGrid();
            _puzzleGrid = new PuzzleGrid(PuzzleGrid, AppGlobals.PuzzleGridRowCount, AppGlobals.PuzzleGridColumnCount);
            InitPuzzleGame(_puzzleGrid);
        }

        private void InitMonsterGrid()
        {
            //var monsterGrid = new MonsterGrid(MonsterGrid);

            //var activeLevel = new Level();
            //monsterGrid.AddLevel(activeLevel);
        }

        private void InitHeroGrid()
        {
            var heroGrid = new HeroGrid(HeroGrid);

            var activeTeam = new Team();
            heroGrid.AddHeroes(activeTeam);
        }

        private async void InitPuzzleGame(PuzzleGrid puzzleGrid)
        {
            _puzzleGame = new PuzzleGameEvents(puzzleGrid);
            await puzzleGrid.MatchAndReplacePuzzlePieces();
            PopupUtils.UncoverScreen();
        }
    }
}