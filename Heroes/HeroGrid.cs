using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Heroes
{
    public class HeroGrid
    {
        private Grid _grid;
        private Team _activeTeam;

        public HeroGrid (Grid heroGrid)
        {
            _grid = GridUtils.AddColumnsToGrid(heroGrid, AppGlobals.MaxHeroesOnATeam);
        }

        public void AddHeroes(Team activeTeam)
        {
            _activeTeam = activeTeam;
            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                AddHero(i, activeTeam.Heroes[i], _grid);
            }
        }

        private void AddHero(int column, Hero hero, Grid grid) 
        {
            if (hero != null)
            {
                var heroProfile = new HeroProfile(hero);
                grid.Children.Add(heroProfile);
                heroProfile.SetValue(Grid.RowProperty, 0);
                heroProfile.SetValue(Grid.ColumnProperty, column);
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
