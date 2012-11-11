using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using LiteApp.Bizcard.Helpers;

namespace LiteApp.Bizcard.Views
{
    public class EnumToDescriptionConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var en = (Enum)value;
            return en.GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
