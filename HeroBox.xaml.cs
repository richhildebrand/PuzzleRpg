using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class HeroBox : PhoneApplicationPage
    {
        private readonly int HEROES_PER_ROW = 5;

        public HeroBox()
        {
            InitializeComponent();
            MessageBus.Default.Register("ShowHeroDetails", OnShowHeroDetails);
            PopupUtils.UncoverScreen(); //just to be safe

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

        private void OnShowHeroDetails(object sender, NotificationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroDetails.xaml", UriKind.RelativeOrAbsolute));
        }

        private void LoadPlayerHeroes(LongListSelector heroGrid)
        {
            var playerHeroes = HeroRepository.GetPlayerHeroes();
            var numberOfHeroSlotsPlayerHasPurchased = 20; // || number Of Heroes (don't lose their drops!)

            var heroSlots = new List<HeroProfileView>();
            for (int i = 0; i < numberOfHeroSlotsPlayerHasPurchased; i++)
            {
                var heroProfile = new HeroProfileView();
                if (playerHeroes.Count > i)
                {
                    var hero = playerHeroes[i];
                    heroProfile.ProfileImageSource = "/" + hero.ProfileImagePath;
                    heroProfile.OrbImageSource = ImageUtils.GetOrbImagePathFromType(hero.Type);
                    heroProfile.BorderImageSource = "/" + ImageUtils.GetProfileBorderImagePathFromType(hero.Type);
                }
                heroSlots.Add(heroProfile);
            }

            heroGrid.GridCellSize = new System.Windows.Size(Application.Current.Host.Content.ActualWidth / HEROES_PER_ROW,
                                                            100);
            heroGrid.ItemsSource = heroSlots;
        }
    }
}