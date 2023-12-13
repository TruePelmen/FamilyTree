using System;
using System.Globalization;
using System.Windows.Data;

namespace FamilyTree.WPF.ViewModel
{
    public class AccessTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string accessType)
            {
                switch (accessType)
                {
                    case "Редагування":
                        return "edit";
                    case "Перегляд":
                        return "view";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string accessType)
            {
                switch (accessType)
                {
                    case "edit":
                        return "Редагування";
                    case "view":
                        return "Перегляд";
                }
            }

            return value;
        }
    }
}
