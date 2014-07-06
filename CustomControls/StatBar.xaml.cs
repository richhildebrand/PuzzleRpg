using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PuzzleRpg.CustomControls
{
    public partial class StatBar : UserControl
    {
        public StatBar()
        {
            InitializeComponent();
        }

        public void SetColor(SolidColorBrush fillColor)
        {
            ThisStatBar.Fill = fillColor;
        }

        public void SetFillPercentage(int current, int total)
        {
            var fillPercentage = GetFillPercentage(current, total);
            StatPercentage.ColumnDefinitions[0].Width = new GridLength(fillPercentage, GridUnitType.Star);
            StatPercentage.ColumnDefinitions[2].Width = new GridLength(100 - fillPercentage, GridUnitType.Star);
        }

        public void SetChangePercentage(int current, int total, int change)
        {
            var previousPercentage = GetFillPercentage(current, total);
            var changePercentage = GetFillPercentage(change, total);

            previousPercentage = previousPercentage - changePercentage;
            previousPercentage = EnsureValidFillPercentage(previousPercentage);

            var filledPercentage = changePercentage + previousPercentage;
            var emptyPercentage = 100 - filledPercentage;
            emptyPercentage = EnsureValidFillPercentage(emptyPercentage);

            StatPercentage.ColumnDefinitions[0].Width = new GridLength(previousPercentage, GridUnitType.Star);
            StatPercentage.ColumnDefinitions[1].Width = new GridLength(changePercentage, GridUnitType.Star);
            StatPercentage.ColumnDefinitions[2].Width = new GridLength(emptyPercentage, GridUnitType.Star);
        }

        private double GetFillPercentage(int current, int total)
        {
            var fillPercentage = ((double)current / (double)total) * 100;
            return EnsureValidFillPercentage(fillPercentage);
        }

        private double EnsureValidFillPercentage(double percentageToVerify) 
        {
            percentageToVerify = double.IsNaN(percentageToVerify) ? 0 : percentageToVerify;
            percentageToVerify = (percentageToVerify < 0) ? 0 : percentageToVerify;
            percentageToVerify = (percentageToVerify > 100) ? 100 : percentageToVerify;
            return percentageToVerify;
        }
    }
}
