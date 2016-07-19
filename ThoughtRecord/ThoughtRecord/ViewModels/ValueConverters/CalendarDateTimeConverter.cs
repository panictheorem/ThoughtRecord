using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ThoughtRecordApp.ViewModels.ValueConverters
{
    /// <summary>
    /// Converts DateTime to format required by calendar picker and back again.
    /// </summary>
    public class CalendarDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime? nullableDateTime = (DateTime?)value;
            //Ensure datetime not outside of range
            if (nullableDateTime == null ||
               nullableDateTime > DateTime.MaxValue.AddYears(-1) ||
               nullableDateTime < DateTimeOffset.MinValue)
            {
                nullableDateTime = DateTime.Now;
            }
            DateTime dateTime = (DateTime)nullableDateTime;
            if (dateTime.Kind != DateTimeKind.Local)
            {
                dateTime = dateTime.ToLocalTime();
            }
            return new Nullable<DateTimeOffset>(dateTime);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return value;
            }
            else
            {
                var selectedDateTime = ((DateTimeOffset?)value).GetValueOrDefault().DateTime;
               
                return selectedDateTime.ToUniversalTime();
            }
        }
    }
}
