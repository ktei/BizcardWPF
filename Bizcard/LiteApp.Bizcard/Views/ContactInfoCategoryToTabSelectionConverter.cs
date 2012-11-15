using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using LiteApp.Bizcard.ViewModels;
using System.Windows;

namespace LiteApp.Bizcard.Views
{
    public class ContactInfoCategoryToTabSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ContactInfoCategory selectedCategory = (ContactInfoCategory)value;
            ContactInfoCategory currentCategory = (ContactInfoCategory)Enum.Parse(typeof(ContactInfoCategory), parameter.ToString());
            return selectedCategory == currentCategory;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool selected = (bool)value;
            if (selected)
            {
                return (ContactInfoCategory)Enum.Parse(typeof(ContactInfoCategory), parameter.ToString());
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
