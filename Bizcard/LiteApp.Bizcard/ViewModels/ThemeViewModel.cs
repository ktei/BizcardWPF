using System.Windows.Media;
using Caliburn.Micro;
using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.ViewModels
{
    public class ThemeViewModel : PropertyChangedBase
    {
        bool _isSelected;

        public ThemeViewModel(ThemeColor color)
        {
            Color = color;
            Brush = GetBrush(color);
        }

        public ThemeColor Color { get; private set; }
        public Brush Brush { get; private set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                }
            }
        }

        // #FFD53333 (Red)
        // #FF6DD533 (Green)
        // #FF41B1E1 (Blue)
        // #FFC333FF (Purple)
        // #FFFFAD54 (Orange)
        static Brush GetBrush(ThemeColor color)
        {
            BrushConverter bc = new BrushConverter();  
            switch (color)
            {
                case ThemeColor.Blue: return (Brush)bc.ConvertFrom("#FF41B1E1");
                case ThemeColor.Green: return (Brush)bc.ConvertFrom("#FF6DD533");
                case ThemeColor.Orange: return (Brush)bc.ConvertFrom("#FFFFAD54");
                case ThemeColor.Purple: return (Brush)bc.ConvertFrom("#FFC333FF");
                case ThemeColor.Red: return (Brush)bc.ConvertFrom("#FFD53333");
                default: return (Brush)bc.ConvertFrom("#FF41B1E1");
            }
        }
    }
}
