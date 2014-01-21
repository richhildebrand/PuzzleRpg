using System;
using System.Linq;
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

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string heroId = "";
            if (NavigationContext.QueryString.TryGetValue("playerOwnedHeroId", out heroId))
            {
                var heroGuid = new Guid(heroId);
                ThisHero = HeroRepository.GetPlayerHeroes().Single(h => h.Id == heroGuid);
                BindHero(ThisHero);
            }
        }
  
        private void BindHero(Hero hero)
        {
            FullHeroImage.Source = ImageUtils.GetImageSourceFromPath(hero.FullImagePath);
        }
    }
}