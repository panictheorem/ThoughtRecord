using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Models;
using Windows.UI.Xaml.Data;

namespace ThoughtRecordApp.ViewModels.ValueConverters
{
    public class EmotionRatingValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (double)((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (int)((double)value);
        }
    }
}
