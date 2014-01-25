﻿using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class HeroDetails : PhoneApplicationPage
    {
        public Hero ThisHero { get; set; }

        public HeroDetails()
        {
            InitializeComponent();
            PopupUtils.UncoverScreen(); //just to be safe
        }

        public void OnScreenTap(object sender, GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroBox.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string heroId = "";
            if (NavigationContext.QueryString.TryGetValue("playerOwnedHeroId", out heroId))
            {
                var heroGuid = new Guid(heroId);
                ThisHero = HeroRepository.GetPlayerHeroes().Single(h => h.Id == heroGuid);
                DrawScreen(ThisHero);
            }
        }

        private void DrawScreen(Hero hero)
        {
            FullHeroImage.Source = ImageUtils.GetImageSourceFromPath("/" + hero.FullImagePath);
            LevelCurrentAndMax.Text = "Level " + "???" + " of " + "???";
            Heal.Text = "Heals for " + hero.HealsFor.ToString();
            HP.Text = "Hit points " + hero.HitPoints.ToString();
            Attack.Text = "Attacks for " + hero.AttackDamage.ToString();
            HeroName.Text = hero.Name;
            Border.ImageSource = ImageUtils.GetImageSourceFromPath(ImageUtils.GetProfileBorderImagePathFromType(hero.Type));
            OrbImage.Source = ImageUtils.GetImageSourceFromPath("/" + ImageUtils.GetOrbImagePathFromType(hero.Type));
        }
    }
}