using System;
using System.Linq;

namespace PuzzleRpg.Monsters
{
    public class Level
    {
        private Monster _stageOne;

        public Level()
        {
            _stageOne = new Monster("hydra", 300, 0);
        }

        public Monster GetStage(int stageNumber)
        {
            return _stageOne;
        }
    }
}