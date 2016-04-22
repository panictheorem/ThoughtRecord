using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Services;
using ThoughtRecordDAL.Infrastructure;
using ThoughtRecordDAL.Models;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordDisplayModel
    {
        public ThoughtRecord ThoughtRecord { get; set; }
        public Situation Situation { get; set; }
        public DeeplyObservableCollection<Emotion> Emotions { get; set; }
        public List<string> DefaultInputText { get; }
        public List<string> EmotionNameSuggestions { get; }
        public Settings Settings { get; }
        public ThoughtRecordDisplayModel()
        {
            ThoughtRecord = new ThoughtRecord();
            DefaultInputText = ThoughtRecordService.GetDefaultInputText();
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
