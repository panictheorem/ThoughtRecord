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
                DateTime dateTime = (DateTime)value;
                //Ensure datetime not outside of range
                if(dateTime < DateTimeOffset.MinValue)
                {
                    dateTime = DateTime.MinValue;
                }
                else if( dateTime > DateTime.MaxValue)
                {
                    dateTime = DateTime.MaxValue;
                }

                return new Nullable<DateTimeOffset>(dateTime);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((DateTimeOffset?)value).GetValueOrDefault().DateTime;
        }
    }
}
