using System;
using System.Linq;
using System.Windows.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Monsters
{
    public class MonsterGrid
    {
        private readonly Grid _grid;

        public MonsterGrid(Grid monsterGrid)
        {
            _grid = monsterGrid;
        }

        public void AddLevel(Level level) 
        {
            var stage = level.GetStage(1);
            //stage will contain multiple monsters
            var monster = stage;
            var monsterImage = ImageUtils.GetImageFromPath(monster.FullImagePath);
            _grid.Children.Add(monsterImage);
        }
    }
}