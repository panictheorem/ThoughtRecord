using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Models;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    public static class ThoughtRecordService
    {
        public static ThoughtRecord GetThoughtRecordById(int id)
        {
            return new ThoughtRecord();
        }

        public static void SaveThoughtRecord(ThoughtRecord thoughtRecord)
        {

        }

        public static List<ThoughtRecord> GetThoughtRecords()
        {
            throw new NotImplementedException();
        }

        public static void PopulateWithDefaultValues(ThoughtRecord thoughtRecord)
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("DefaultInputText");
            thoughtRecord.Situation.DateTime = DateTime.Now;
            thoughtRecord.Emotions.Add(new Emotion { Name = resources.GetString("EmotionExample") });
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
            resources.GetString("ContradictingEvidence"),
            resources.GetString("RationalAssessment")
        };
        }
    }
}
