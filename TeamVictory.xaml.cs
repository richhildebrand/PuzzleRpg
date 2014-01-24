﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class TeamVictory : PhoneApplicationPage
    {
        public TeamVictory()
        {
            InitializeComponent();
            PopupUtils.UncoverScreen();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DrawScreen();
        //Looking at the others, I think "DrawScreen" should be our "naming convention" for this type of thing. Well done.
        private void DrawScreen()
        {
            var fontSize = 25;
            //Ask Craig/Caitlin where they download their fonts from (win phone defaults suck)- Line to add:
            //LevelCompleteTextBlock.FontFamily = new System.Windows.Media.FontFamily(".\Fonts\Font_.ttf#Font Name");
            var fontFamily = "Georgia";

            LevelCompleteTextBlock.Text = "Level Complete!";
            LevelCompleteTextBlock.FontSize = 50;
            LevelCompleteTextBlock.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

            SummaryTextBlock.Text = "Summary: ";
            SummaryTextBlock.FontSize = 35;
            SummaryTextBlock.Margin = new System.Windows.Thickness(0, 40, 0, 40);
            SummaryTextBlock.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

            CurrentLevelTextBlock.Text = "Current Level: " + GenerateMeARandomNumberForLevel().ToString();
            CurrentLevelTextBlock.FontSize = fontSize;
            CurrentLevelTextBlock.Margin = new System.Windows.Thickness(0, 0, 0, 20);
            CurrentLevelTextBlock.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

            ExperienceGainedTextBlock.Text = "Total Experienced Gained: " + GenerateMeARandomNumberForExperience().ToString();
            ExperienceGainedTextBlock.FontSize = fontSize;
            ExperienceGainedTextBlock.Margin = new System.Windows.Thickness(0, 0, 0, 20);
            ExperienceGainedTextBlock.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

            MonstersSlainTextBlock.Text = "Monsters Slain: " + GenerateMeARandomNumberForMonstersSlain().ToString();
            MonstersSlainTextBlock.FontSize = fontSize;
            MonstersSlainTextBlock.Margin = new System.Windows.Thickness(0, 0, 0, 20);
            MonstersSlainTextBlock.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

            LootObtainedTextBlock.Text = "Loot Obtained: " + GeneraateMeARandomListForLootObtained();
            LootObtainedTextBlock.FontSize = fontSize;
            LootObtainedTextBlock.Margin = new System.Windows.Thickness(0, 0, 0, 20);
            LootObtainedTextBlock.FontFamily = new System.Windows.Media.FontFamily(fontFamily);

        }

        //Creating a method that generates a random item for now
        //Basically a placeholder for when I create the function "GetLootObtained()"
        private string GeneraateMeARandomListForLootObtained()
        {
            Random randomItem = new Random();
            var loot = new List<string> { "Green Powerup", "Blue Powerup", "Purple Powerup" };
            var itemToReturn = randomItem.Next(loot.Count);
            return loot[itemToReturn];
        }

        //Creating a method that generates a random number for now.
        //Basically a placeholder for when I create the function "GetCurrentLevel()"
        private int GenerateMeARandomNumberForLevel()
        {
            Random randoNumb = new Random();
            int myNmumber = randoNumb.Next(1, 10);
            return myNmumber;
        }

        //Creating a method that generates a random number for now.
        //Basically a placeholder for when I create the function "GetTotalExperienceGained()"
        private int GenerateMeARandomNumberForExperience()
        {
            Random randoNumb = new Random();
            int myNmumber = randoNumb.Next(1000, 45000);
            return myNmumber;
        }

        //Creating a method that generates a random number for now.
        //Basically a placeholderor when I create the function "GetNumberOfMonstersSlain()"
        private int GenerateMeARandomNumberForMonstersSlain()
        {
            Random randoNumb = new Random();
            int myNmumber = randoNumb.Next(0, 20);
            return myNmumber;
        }
    }
}