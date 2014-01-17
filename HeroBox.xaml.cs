using System;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;

namespace PuzzleRpg
{
    public partial class HeroBox : PhoneApplicationPage
    {
        public HeroBox()
        {
            InitializeComponent();

            // TODO: move somewhere that makes more sense
            if (HeroRepository.GetPlayerHeroes().Count == 0)
            {
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(0));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(1));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(2));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(3));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(4));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(5));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(6));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(7));
            }

            LoadPlayerHeroes(HeroGrid);
        }

        private void LoadPlayerHeroes(Grid heroGrid)
        {
            var playerHeroes = HeroRepository.GetPlayerHeroes();
            foreach (var hero in playerHeroes)
            {
                var heroProfile = new HeroProfile();
                heroProfile.DrawHeroProfile(hero);
                heroGrid.Children.Add(heroProfile);
            }

        }
    }
}