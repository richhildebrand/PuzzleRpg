using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class HeroBox : PhoneApplicationPage
    {
        private readonly int HEROES_PER_ROW = 5;

        public HeroBox()
        {
            InitializeComponent();

            // TODO: move somewhere that makes more sense
            //if (HeroRepository.GetPlayerHeroes().Count == 0)
            //{
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(0));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(1));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(2));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(3));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(4));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(5));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(6));
                HeroRepository.AddPlayerHero(HeroDatabase.GetHero(7));
            //}

            LoadPlayerHeroes(HeroGrid);
        }

        private void LoadPlayerHeroes(Grid heroGrid)
        {
            var playerHeroes = HeroRepository.GetPlayerHeroes();
            var numberOfRowsToDisplay = playerHeroes.Count / HEROES_PER_ROW;

            GridUtils.AddColumnsToGrid(heroGrid, HEROES_PER_ROW);
            GridUtils.AddRowsToGrid(heroGrid, numberOfRowsToDisplay);

            for (int i = 0; i < playerHeroes.Count; i++)
            {
                var heroProfile = new HeroProfile();
                heroProfile.DrawHeroProfile(playerHeroes[i]);
                heroGrid.Children.Add(heroProfile);
                heroProfile.SetValue(Grid.RowProperty, i / HEROES_PER_ROW);
                heroProfile.SetValue(Grid.ColumnProperty, i);
                heroProfile.Height = Application.Current.Host.Content.ActualWidth / 5;
            }
        }
    }
}