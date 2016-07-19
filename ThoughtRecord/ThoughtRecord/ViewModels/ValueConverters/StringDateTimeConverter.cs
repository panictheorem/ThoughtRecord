using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ThoughtRecordApp.ViewModels.ValueConverters
{
    /// <summary>
    /// Converts DateTime to be displayed as a string and back to DateTime
    /// </summary>
    class StringDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Kind != DateTimeKind.Local)
            {
                dateTime = dateTime.ToLocalTime();
            }
            return dateTime.ToString("MMMM dd, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DateTime.Parse(value as string).ToUniversalTime();
        }
    }
}
