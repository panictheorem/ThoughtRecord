using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ThoughtRecordApp.ViewModels.ValueConverters
{
    /// <summary>
    /// Converter to display emotion name with a colon in the UI
    /// </summary>
    public class EmotionNameDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
                string emotionName = value as string;
                if(!string.IsNullOrWhiteSpace(emotionName))
                {
                    return emotionName + " :";
                }

                return "unspecified :";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
