using System;
using System.Linq;
using System.Windows.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Heroes
{
    public class HeroGrid
    {
        private Grid _grid;

        public HeroGrid (Grid heroGrid)
        {
            _grid = GridUtils.AddColumnsToGrid(heroGrid, AppGlobals.MaxHeroesOnATeam);
        }

        public void AddHeroes(Team activeTeam)
        {
            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                AddHero(i, activeTeam.Heroes[i], _grid);
            }
        }

        private void AddHero(int column, Hero hero, Grid grid) 
        {
            if (hero != null)
            {
                grid.Children.Add(hero.Element);
                hero.Element.SetValue(Grid.ColumnProperty, column);
                hero.Element.SetValue(Grid.RowProperty, 0);
                hero.Element.Stretch = System.Windows.Media.Stretch.Fill;
            }
        }
    }
}
