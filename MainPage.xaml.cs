using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Heroes;
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
        }

        private void LoadGraphics(object sender, RoutedEventArgs e)
        {
            InitHeroGrid();
            InitPuzzleGrid();
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
            _puzzleGrid.AddOrbs();
        }

        private void MouseMoving(object sender, MouseEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 1);
        }
        
        public void OnDragEnd(object sender, ManipulationCompletedEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 0);
        }
    }
}