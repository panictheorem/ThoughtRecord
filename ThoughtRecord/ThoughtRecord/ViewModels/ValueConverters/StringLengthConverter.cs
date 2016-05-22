using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ThoughtRecordApp.ViewModels.ValueConverters
{

    /// <summary>
    /// Truncates and removes carriage returns from string and appends "..." if it is over 150 characters.
    /// </summary>
    class StringLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string str = value as string;
            //remove new lines
            if(str.Contains("\r\n"))
            {
                str = Regex.Replace(str, "\r\n", m => " ");
            }
            //truncate if longer than 100 characters
            if (str.Length > 100)
            {
                str = str.Substring(0, 99);
                str += "...";
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
