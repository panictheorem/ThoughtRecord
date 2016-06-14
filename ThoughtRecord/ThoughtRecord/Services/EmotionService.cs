using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    public static class EmotionService
    {
        public static List<string> GetEmotionNameSuggestions()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("DefaultInputText");
            return resources.GetString("EmotionSuggestions").Split(',').ToList();
        }
    }
}
