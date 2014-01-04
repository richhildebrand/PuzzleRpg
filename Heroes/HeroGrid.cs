using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
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
                var heroIcon = ImageUtils.GetImageFromPath(hero.ProfileImagePath);
                heroIcon.Tag = hero;
                grid.Children.Add(heroIcon);
                heroIcon.SetValue(Grid.ColumnProperty, column);
                heroIcon.SetValue(Grid.RowProperty, 0);
                heroIcon.Stretch = System.Windows.Media.Stretch.Fill;
                heroIcon.Tap += OnSelectHero;
            }
        }
  
        private void OnSelectHero(object sender, GestureEventArgs e)
        {
            var clickedIcon = sender as Image;
            var selectedHero = clickedIcon.Tag as Hero;
            var castSpellModal = new CastSpellModal(selectedHero);
            castSpellModal.Show();
        }
    }
}
