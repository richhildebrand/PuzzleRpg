using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class HeroToViewModelMapper
    {
        public static List<HeroViewModel> GetHeroViewModels(List<Hero> heroes) {
            Hero[] heroArray = heroes.ToArray();
            var heroViewModels = GetHeroViewModels(heroArray);

            return heroViewModels.ToList();
        }

        public static HeroViewModel[] GetHeroViewModels(Hero[] heroes)
        {
            var heroViewModels = new HeroViewModel[heroes.Length];

            for (int i = 0; i < heroViewModels.Length; i++)
            {
                heroViewModels[i] = GetHeroViewModel(heroes[i]);
            }

            return heroViewModels;
        }

        private static HeroViewModel GetHeroViewModel(Hero hero) {
            var heroVM = new HeroViewModel();

            if (hero != null)
            {
                heroVM = PopulateValuesOnHeroVM(hero);
            }

            return heroVM;
        }

        private static HeroViewModel PopulateValuesOnHeroVM(Hero hero)
        {
            var heroVM = new HeroViewModel();
            heroVM.ProfileImageSource = "/" + hero.ProfileImagePath;
            heroVM.OrbImageSource = ImageUtils.GetOrbImagePathFromType(hero.Type);
            heroVM.BorderImageSource = "/" + ImageUtils.GetProfileBorderImagePathFromType(hero.Type);
            heroVM.Id = hero.Id.ToString();

            return heroVM;
        }
    }
}
