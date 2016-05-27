using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.ViewModels;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    /// <summary>
    /// Retrives string data related to the thought record UI
    /// </summary>
    public static class ThoughtRecordService
    {

        public static void PopulateWithDefaultValues(ThoughtRecord thoughtRecord)
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("DefaultInputText");
            thoughtRecord.Situation.DateTime = DateTime.Now;
            thoughtRecord.Emotions.Add(new Emotion { Name = resources.GetString("EmotionExample"), InitialRating = 50,  SubsequentRating = 50});
            thoughtRecord.Situation.Description = resources.GetString("SituationDescription");
            thoughtRecord.AutomaticThoughts = resources.GetString("AutomaticThoughts");
            thoughtRecord.SupportingEvidence = resources.GetString("SupportingEvidence");
            thoughtRecord.ContradictingEvidence = resources.GetString("ContradictingEvidence");
            thoughtRecord.RationalAssessment = resources.GetString("RationalAssessment");
        }

        public static List<string> GetDefaultInputText()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("DefaultInputText");
            return new List<string>
            {
            resources.GetString("SituationDescription"),
            resources.GetString("AutomaticThoughts"),
            resources.GetString("SupportingEvidence"),
            resources.GetString("EmotionExample"),
            resources.GetString("ContradictingEvidence"),
            resources.GetString("RationalAssessment")
        };
        }

        public static ThoughtRecordSectionTitleModel GetTitleModel()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("ThoughtRecordTitles");
            return new ThoughtRecordSectionTitleModel
            {
                SituationTitle = resources.GetString("SituationTitle"),
                InitialEmotionsTitle = resources.GetString("InitialEmotionsTitle"),
                AutomaticThoughtsTitle = resources.GetString("AutomaticThoughtsTitle"),
                EvidenceAgainstThoughtsTitle = resources.GetString("EvidenceAgainstThoughtsTitle"),
                EvidenceForThoughtsTitle = resources.GetString("EvidenceForThoughtsTitle"),
                RationalAssessment = resources.GetString("RationalAssessment"),
                SubsequentEmotionsTitle = resources.GetString("SubsequentEmotionsTitle")
            };
        }
    }
}
