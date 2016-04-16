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
        public List<string> EmotionNameSuggestions { get; }
        public Settings Settings { get; set; }
        public ThoughtRecordDisplayModel()
        {
            ThoughtRecord = new ThoughtRecord();
            EmotionNameSuggestions = EmotionService.GetEmotionNameSuggestions();
            Settings = new Settings();
            ThoughtRecordService.PopulateWithDefaultValues(ThoughtRecord);
        }
        public ThoughtRecordDisplayModel(int thoughtRecordId)
        {

            ThoughtRecord = ThoughtRecordService.GetThoughtRecordById(thoughtRecordId);
            EmotionNameSuggestions = EmotionService.GetEmotionNameSuggestions();
            Settings = new Settings();
            ThoughtRecord.Situation.Description = "Saved Desc";
            ThoughtRecord.AutomaticThoughts = "SAVED Describe the automatic throught(s) you had...";
            ThoughtRecord.SupportingEvidence = "SAVED List the evidence that supports the automatic thought...";
            ThoughtRecord.ContradictingEvidence = "SAVED List the evidence againts the automatic thought...";
            ThoughtRecord.RationalAssessment = "SAVED Come to a rational conclusion...";

        }
    }
}
