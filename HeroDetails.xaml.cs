using System;
using System.Linq;
using Microsoft.Phone.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class HeroDetails : PhoneApplicationPage
    {
        public HeroDetails()
        {
            InitializeComponent();
            PopupUtils.UncoverScreen(); //just to be safe
        }
    }
}