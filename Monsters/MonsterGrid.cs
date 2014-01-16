using System;
using System.Linq;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Monsters
{
    public class MonsterGrid
    {
        private readonly MonsterWithHealthBar _monsterUI;
        public Monster ActiveMonster { get; set; }

        public MonsterGrid(MonsterWithHealthBar monsterUI)
        {
            _monsterUI = monsterUI;
            var monster = MonsterDatabase.GetMonster(0);
            ActivateMonster(monster);
        }

        private void ActivateMonster(Monster monster)
        {
            _monsterUI.MonsterImage.Source = ImageUtils.GetImageSourceFromPath("/" + monster.FullImagePath);
            ActiveMonster = monster;
        }
    }
}