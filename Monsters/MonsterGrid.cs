using System;
using System.Linq;
using System.Windows.Input;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Monsters
{
    public class MonsterGrid
    {
        private readonly MonsterWithHealthBar _monsterUI;
        public Monster ActiveMonster { get; set; }

        private int HACK_currentMonster = 0;//TODO: REMOVE THIS!!!
        private void HACK_ToggleMonsterImage(object sender, GestureEventArgs e)
        {
            HACK_currentMonster += 1;
            HACK_currentMonster = (HACK_currentMonster >= MonsterDatabase.MonsterCount()) ? 0 : HACK_currentMonster;
            var monster = MonsterDatabase.GetMonster(HACK_currentMonster);
            _monsterUI.MonsterImage.Source = ImageUtils.GetImageSourceFromPath("/" + monster.FullImagePath);
        }

        public MonsterGrid(MonsterWithHealthBar monsterUI)
        {
            _monsterUI = monsterUI;
            var monster = MonsterDatabase.GetMonster(0);
            ActivateMonster(monster);
        }

        private void ActivateMonster(Monster monster)
        {
            _monsterUI.MonsterImage.Source = ImageUtils.GetImageSourceFromPath("/" + monster.FullImagePath);
            _monsterUI.Tap += HACK_ToggleMonsterImage;
            ActiveMonster = monster;
        }
    }
}