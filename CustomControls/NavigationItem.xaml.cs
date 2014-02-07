using System;
using System.Linq;
using System.Windows.Controls;

namespace PuzzleRpg.CustomControls
{
    public partial class NavigationItem : UserControl
    {
        public string navigationItemText { get; set; }

        public NavigationItem()
        {
            InitializeComponent();
        }

        public void GenerateNavItem(NavigationItem item)
        {
            NavItemText.Text = navigationItemText;
        }
    }
}
