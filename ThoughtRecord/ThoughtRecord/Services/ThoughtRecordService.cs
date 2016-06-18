using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.Infrastructure.Interfaces;
using ThoughtRecordApp.ViewModels;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    /// <summary>
    /// Retrives string data related to the thought record UI
    /// </summary>
    public class ThoughtRecordService
    {
        private IStringResourceService stringLoader;

        private void InitializeOrUpdateStringLoader(string fileName)
        {
            if(stringLoader == null)
            {
                stringLoader = new StringResourceService(fileName);
            }
            else
            {
                stringLoader.SetFile(fileName);
            }
        }
        public void PopulateWithDefaultValues(ThoughtRecord thoughtRecord)
        {
            InitializeOrUpdateStringLoader("DefaultInputText");
            thoughtRecord.Situation.DateTime = DateTime.Now;
            thoughtRecord.Emotions.Add(new Emotion { Name = stringLoader.GetString("EmotionExample"), InitialRating = 50,  SubsequentRating = 50});
            thoughtRecord.Situation.Description = stringLoader.GetString("SituationDescription");
            thoughtRecord.AutomaticThoughts = stringLoader.GetString("AutomaticThoughts");
            thoughtRecord.SupportingEvidence = stringLoader.GetString("SupportingEvidence");
            thoughtRecord.ContradictingEvidence = stringLoader.GetString("ContradictingEvidence");
            thoughtRecord.RationalAssessment = stringLoader.GetString("RationalAssessment");
        }

        public List<string> GetDefaultInputText()
        {
            InitializeOrUpdateStringLoader("DefaultInputText");
            return new List<string>
            {
            stringLoader.GetString("SituationDescription"),
            stringLoader.GetString("AutomaticThoughts"),
            stringLoader.GetString("SupportingEvidence"),
            stringLoader.GetString("EmotionExample"),
            stringLoader.GetString("ContradictingEvidence"),
            stringLoader.GetString("RationalAssessment")
        };
        }

        public ThoughtRecordSectionTitleModel GetTitleModel()
        {
            InitializeOrUpdateStringLoader("ThoughtRecordTitles");
            return new ThoughtRecordSectionTitleModel
            {
                SituationTitle = stringLoader.GetString("SituationTitle"),
                InitialEmotionsTitle = stringLoader.GetString("InitialEmotionsTitle"),
                AutomaticThoughtsTitle = stringLoader.GetString("AutomaticThoughtsTitle"),
                EvidenceAgainstThoughtsTitle = stringLoader.GetString("EvidenceAgainstThoughtsTitle"),
                EvidenceForThoughtsTitle = stringLoader.GetString("EvidenceForThoughtsTitle"),
                RationalAssessment = stringLoader.GetString("RationalAssessment"),
                SubsequentEmotionsTitle = stringLoader.GetString("SubsequentEmotionsTitle")
            };
        }
    }
}
