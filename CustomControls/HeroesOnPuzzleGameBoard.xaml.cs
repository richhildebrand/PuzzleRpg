using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Logic;
using PuzzleRpg.Models;

namespace PuzzleRpg.CustomControls
{
    public partial class HeroesOnPuzzleGameBoard : UserControl
    {
        private Team _activeTeam;

        public HeroesOnPuzzleGameBoard()
        {
            InitializeComponent();
        }

        public void AddHeroes(Team activeTeam)
        {
            this._activeTeam = activeTeam;
            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                AddHero(i, activeTeam.Heroes[i], LayoutRoot);
            }
        }

        private void AddHero(int column, Hero hero, Grid grid)
        {
            if (hero != null)
            {
                var heroProfile = new HeroProfile();
                heroProfile.DrawHeroProfile(hero);
                grid.Children.Add(heroProfile);
                heroProfile.SetValue(Grid.RowProperty, 0);
                heroProfile.SetValue(Grid.ColumnProperty, column*2);
                heroProfile.Tap += OnSelectHero;
            }
        }

        private void OnSelectHero(object sender, GestureEventArgs e)
        {
            var selectedHeroProfile = sender as HeroProfile;
            var castSpellModal = new CastSpellModal(selectedHeroProfile.ThisHero);
            castSpellModal.Show();
        }
    }
}
