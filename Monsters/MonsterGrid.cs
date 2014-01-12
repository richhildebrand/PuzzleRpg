using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Monsters
{
    public class MonsterGrid
    {
        private readonly Grid _grid;

        public MonsterGrid(Grid monsterGrid)
        {
            _grid = monsterGrid;
            _grid = GridUtils.AddRowsToGrid(monsterGrid, 2);

            var monsterPictureRow = _grid.RowDefinitions[0];
            var monsterHealthRow = _grid.RowDefinitions[1];
            monsterPictureRow.Height = new System.Windows.GridLength(9, System.Windows.GridUnitType.Star);
            monsterHealthRow.Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
        }

        public void AddLevel(Level level) 
        {
            var stage = level.GetStage(1);
            //stage can contain multiple monsters
            var monster = stage;

            var monsterImage = ImageUtils.GetImageFromPath(monster.FullImagePath);
            _grid.Children.Add(monsterImage);
            monsterImage.SetValue(Grid.RowProperty, 0);

            var healthBar = DrawHealthBar(monster.HitPoints);
            _grid.Children.Add(healthBar);
            healthBar.SetValue(Grid.RowProperty, 1);
        }

        private Line DrawHealthBar(int lenghtOfHealthBar)
        {
            Line line = new Line();
            line.X1 = 0;
            line.X2 = lenghtOfHealthBar;
            line.Stroke = new SolidColorBrush(Colors.Magenta);
            return line;
        }
    }
}