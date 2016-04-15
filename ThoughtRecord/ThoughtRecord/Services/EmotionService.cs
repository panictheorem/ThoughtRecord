using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.Services
{
    public static class EmotionService
    {
        public static List<string> GetEmotionNameSuggestions()
        {
            return new List<string>
            {
                "Happy",
                "Sad",
                "Scared",
                "Frustrated",
                "Angry",
                "Anxious",
                "Depressed",
                "Stressed"
            };
        }
    }
}
