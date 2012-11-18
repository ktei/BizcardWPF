using LiteApp.Bizcard.Models;

namespace LiteApp.Bizcard.ViewModels
{
    public class ThemeMessage
    {
        public ThemeMessage(ThemeColor color)
        {
            Color = color;
        }

        public ThemeColor Color { get; private set; }
    }
}
