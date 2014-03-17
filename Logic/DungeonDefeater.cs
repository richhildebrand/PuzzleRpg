using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Database;
using PuzzleRpg.Models;

namespace PuzzleRpg.Logic
{
    public class DungeonDefeater
    {
        private readonly Dungeon _defeatedDungeon;

        public DungeonDefeater(Dungeon defeatedDungeon)
        {
            _defeatedDungeon = defeatedDungeon;
        }

        public void Defeat()
        {
            var dungeonRepository = new DungeonRepository();
            var dungeons = dungeonRepository.GetAllDungeons();
            SetDungeonAsCleared(dungeons);
            UnlockNextDungeon(dungeons);
            dungeonRepository.Save(dungeons);
        }

        private void SetDungeonAsCleared(List<Dungeon> dungeons)
        {
            var defeatedDungeonId = _defeatedDungeon.Id;
            var defeatedDungeon = dungeons.Single(d => d.Id == defeatedDungeonId);
            defeatedDungeon.HasBeenDefeated = true;
        }

        private void UnlockNextDungeon(List<Dungeon> dungeons)
        {
            var dungeonToUnlockId = _defeatedDungeon.Unlocks;
            var dungeonToUnlock = dungeons.SingleOrDefault(d => d.Id == dungeonToUnlockId);
            if (dungeonToUnlock != null)
            {
                dungeonToUnlock.IsAvailable = true;
            }
        }
    }
}
