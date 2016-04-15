using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ThoughtRecordApp.ViewModels.ValueConverters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new Nullable<DateTimeOffset>((DateTime)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((DateTimeOffset?)value).GetValueOrDefault().DateTime;
        }
    }
}
