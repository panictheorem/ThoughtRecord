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
                DateTime? dateTime = (DateTime?)value;
                //Ensure datetime not outside of range
                if(dateTime == null ||
                   dateTime > DateTime.MaxValue.AddYears(-1) ||
                   dateTime < DateTimeOffset.MinValue)
                {
                    dateTime = DateTime.Now;
                }

                return new Nullable<DateTimeOffset>((DateTime)dateTime);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
            {
                return value;
            }
            else
            {
                var selectedDateTime = ((DateTimeOffset?)value).GetValueOrDefault().DateTime;
                //There are issues with how SQLite stores the datetime as ticks
                //Date is stored 4 hours ahead which can make the date the day after the user intended
                //This solution to the problem creates a new date with a midday time to work around this
                return new DateTime(year: selectedDateTime.Year,
                                    month: selectedDateTime.Month,
                                    day: selectedDateTime.Day,
                                    hour: 12,
                                    minute: 1,
                                    second: 1,
                                    millisecond: 1, 
                                    kind: DateTimeKind.Utc);
            }
        }
    }
}
