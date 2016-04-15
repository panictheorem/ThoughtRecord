using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Services;
using ThoughtRecordDAL.Models;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordDisplayModel
    {
        public ThoughtRecord ThoughtRecord { get; set; }
        public List<string> EmotionNameSuggestions { get;}
        public Settings Settings { get; set; }

        public ThoughtRecordDisplayModel()
        {
            ThoughtRecord = new ThoughtRecord();
            EmotionNameSuggestions = EmotionService.GetEmotionNameSuggestions();
            Settings = new Settings();
            ThoughtRecord.Situation.DateTime = DateTime.Now;
            ThoughtRecord.Situation.Description = "Describe the situation...";
            ThoughtRecord.AutomaticThoughts = "Describe the automatic throught(s) you had...";
            ThoughtRecord.SupportingEvidence = "List the evidence that supports the automatic thought...";
            ThoughtRecord.ContradictingEvidence = "List the evidence againts the automatic thought...";
            ThoughtRecord.RationalAssessment = "Come to a rational conclusion...";

        }
    }
}
