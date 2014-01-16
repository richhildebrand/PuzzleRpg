using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Monsters;

namespace PuzzleRpg.Database
{
    public static class MonsterDatabase
    {
        private static List<Monster> _allMonsters;

        static MonsterDatabase()
        {
            _allMonsters = new List<Monster>();
                                         //Name   HP   Attack
            _allMonsters.Add(new Monster("hydra", 300, 10));
        }

        public static Monster GetMonster(int monsterId)
        {
            var monsterFromDatabase = _allMonsters[monsterId];
            return CopyMonster(monsterFromDatabase);
        }

        private static Monster CopyMonster(Monster monsterToCopy)
        {
            return new Monster(monsterToCopy.Name,
                               monsterToCopy.HitPoints,
                               monsterToCopy.AttackDamage);
        }
    }
}
