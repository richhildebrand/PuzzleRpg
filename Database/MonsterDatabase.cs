using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public static class MonsterDatabase
    {
        private static List<Monster> _allMonsters;

        static MonsterDatabase()
        {
            _allMonsters = new List<Monster>();

            _allMonsters.Add(new Monster("BlueDragon", 1000, 1000, 100));
            _allMonsters.Add(new Monster("GreenDragon", 1000, 90, 100));
            _allMonsters.Add(new Monster("RedDragon", 1000, 90, 100));
            _allMonsters.Add(new Monster("BigBlue", 300, 20, 30));
            _allMonsters.Add(new Monster("Turtle", 5000, 1, 30));
            _allMonsters.Add(new Monster("Rex", 1000, 10, 10));
            _allMonsters.Add(new Monster("StoneOrc", 500, 30, 10));
            _allMonsters.Add(new Monster("WoodOrc", 500, 30, 10));
        }

        public static int MonsterCount()
        {
            return _allMonsters.Count();
        }

        public static Monster GetMonster(int monsterId)
        {
            var monsterFromDatabase = _allMonsters[monsterId];
            return CopyMonster(monsterFromDatabase);
        }

        public static Monster GetMonster(string monsterName)
        {
            var monsterFromDatabase = _allMonsters.Single(m => m.Name == monsterName);
            return CopyMonster(monsterFromDatabase);
        }

        private static Monster CopyMonster(Monster monsterToCopy)
        {
            return new Monster(monsterToCopy.Name,
                               monsterToCopy.TotalHealth,
                               monsterToCopy.AttackDamage,
                               monsterToCopy.ExpGivenOnDeath);
        }
    }
}
