﻿using System;
using System.Linq;
using System.Threading.Tasks;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Models;
using PuzzleRpg.Monsters;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Logic
{
    public class MonsterGrid
    {
        private readonly MonsterWithHealthBar _monsterUI;
        private readonly Dungeon _dungeon;
        private DungeonFloor _activeFloor;
        
        public HealthBar MonsterHealth { get; set; }
        public Monster ActiveMonster { get; set; }

        //private int HACK_currentMonster = 0;//TODO: REMOVE THIS!!!
        //private void HACK_ToggleMonsterImage(object sender, GestureEventArgs e)
        //{
        //    HACK_currentMonster += 1;
        //    HACK_currentMonster = (HACK_currentMonster >= MonsterDatabase.MonsterCount()) ? 0 : HACK_currentMonster;
        //    var monster = MonsterDatabase.GetMonster(HACK_currentMonster);
        //    _monsterUI.MonsterImage.Source = ImageUtils.GetImageSourceFromPath("/" + monster.FullImagePath);
        //}

        public MonsterGrid(MonsterWithHealthBar monsterUI, Dungeon dungeon)
        {
            _monsterUI = monsterUI;
            MonsterHealth = _monsterUI.MonsterHealth;

            _dungeon = dungeon;
            _activeFloor = _dungeon.Floors[0];
            ActivateMonster(_activeFloor.Monsters);
        }
        
        public bool LoadNextFloor()
        {
            var hasAnotherFloor = false;
            var currentFloorIndex = _dungeon.Floors.IndexOf(_activeFloor);

            var nextFloorIndex = currentFloorIndex + 1;
            if (nextFloorIndex < _dungeon.Floors.Count)
            {
                _activeFloor = _dungeon.Floors[nextFloorIndex];
                ActivateMonster(_activeFloor.Monsters);
                hasAnotherFloor = true;
            }

            return hasAnotherFloor;
        }

        private Task ActivateMonster(Monster monster)
        {
            _monsterUI.MonsterImage.Source = ImageUtils.GetImageSourceFromPath("/" + monster.FullImagePath);
            //_monsterUI.Tap += HACK_ToggleMonsterImage;
            ActiveMonster = monster;
            return MonsterHealth.SetHealthPercentage(monster.CurrentHealth, monster.TotalHealth);
        }
    }
}