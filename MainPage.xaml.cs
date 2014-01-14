using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Heroes;
using PuzzleRpg.Monsters;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class MainPage : PhoneApplicationPage
    {
        PuzzleGrid _puzzleGrid;

        public MainPage()
        {
            InitializeComponent();
            Loaded += LoadGraphics;
            PopupUtils.CoverScreen(100); //lol loading mask...
        }

        private void LoadGraphics(object sender, RoutedEventArgs e)
        {
            InitMonsterGrid();
            InitHeroGrid();
            InitPuzzleGrid();
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

        private void InitPuzzleGrid()
        {
            _puzzleGrid = new PuzzleGrid(PuzzleGrid, AppGlobals.PuzzleGridRowCount, AppGlobals.PuzzleGridColumnCount);
            _puzzleGrid.EndingTurn();
        }
    }
}