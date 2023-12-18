namespace FamilyTree.WPF.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class AccessTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ComboBoxItem comboBoxItem)
            {
                value = comboBoxItem.Content;
            }

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
    }
}