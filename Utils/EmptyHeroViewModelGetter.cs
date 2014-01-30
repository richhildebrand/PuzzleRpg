using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class EmptyHeroViewModelGetter
    {
        public static List<HeroViewModel> GetEmptyHeroViewModel(int numberOfEmptyProfilesToGet)
        {
            var additionalHeroVMs = FillEmptySlots(numberOfEmptyProfilesToGet);
            return additionalHeroVMs;
        }

        private static List<HeroViewModel> FillEmptySlots(int extraSlotsToAdd)
        {
            var emptyHeroVMs = new List<HeroViewModel>();
            if (extraSlotsToAdd > 0)
            {
                var emptySlots = new Hero[extraSlotsToAdd];
                emptyHeroVMs = HeroToViewModelMapper.GetHeroViewModels(emptySlots).ToList();
            }

            return emptyHeroVMs;
        }
    }
}
