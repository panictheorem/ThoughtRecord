using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Infrastructure.Interfaces;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    public static class EmotionService
    {
        public static List<string> GetEmotionNameSuggestions()
        {
            IStringResourceService stringLoader = new StringResourceService("DefaultInputText");
            return stringLoader.GetString("EmotionSuggestions").Split(',').ToList();
        }
    }
}
