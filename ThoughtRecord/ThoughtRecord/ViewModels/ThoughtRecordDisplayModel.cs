using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Services;
using ThoughtRecordApp.ViewModels.Infrastructure;
using ThoughtRecordDAL.Infrastructure;
using ThoughtRecordDAL.Models;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordDisplayModel : BindableBase
    {
        private ThoughtRecord thoughtRecord;
        public DateTime SituationDateTime
        {
            get
            {
                return thoughtRecord.Situation.DateTime;
            }
            set
            {
                thoughtRecord.Situation.DateTime = value;
                OnPropertyChanged("DateTime");
            }
        }
        public string SituationDescription
        {
            get
            {
                return thoughtRecord.Situation.Description;
            }
            set
            {
                thoughtRecord.Situation.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public string AutomaticThoughts
        {
            get
            {
                return thoughtRecord.AutomaticThoughts;
            }
            set
            {
                thoughtRecord.AutomaticThoughts = value;
                OnPropertyChanged("Description");
            }
        }
        public string SupportingEvidence
        {
            get
            {
                return thoughtRecord.SupportingEvidence;
            }
            set
            {
                thoughtRecord.SupportingEvidence = value;
                OnPropertyChanged("SupportingEvidence");
            }
        }
        public string ContradictingEvidence
        {
            get
            {
                return thoughtRecord.ContradictingEvidence;
            }
            set
            {
                thoughtRecord.ContradictingEvidence = value;
                OnPropertyChanged("ContradictingEvidence");
            }
        }
        public string RationalAssessment
        {
            get
            {
                return thoughtRecord.RationalAssessment;
            }
            set
            {
                thoughtRecord.RationalAssessment = value;
                OnPropertyChanged("RationalAssessment");
            }
        }
        public DeeplyObservableCollection<Emotion> Emotions
        {
            get
            {
                return thoughtRecord.Emotions;
            }
            set
            {
                thoughtRecord.Emotions = value;
            }
        }
        public List<string> DefaultInputText { get; }
        public Settings Settings { get; }

        public ThoughtRecordDisplayModel()
        {
            thoughtRecord = new ThoughtRecord();
            DefaultInputText = ThoughtRecordService.GetDefaultInputText();
            Settings = new Settings();
            ThoughtRecordService.PopulateWithDefaultValues(thoughtRecord);
        }
        public ThoughtRecordDisplayModel(int thoughtRecordId)
        {

            thoughtRecord = ThoughtRecordService.GetThoughtRecordById(thoughtRecordId);
            Settings = new Settings();
            thoughtRecord.Situation.Description = "Saved Desc";
            thoughtRecord.AutomaticThoughts = "SAVED Describe the automatic throught(s) you had...";
            thoughtRecord.SupportingEvidence = "SAVED List the evidence that supports the automatic thought...";
            thoughtRecord.ContradictingEvidence = "SAVED List the evidence againts the automatic thought...";
            thoughtRecord.RationalAssessment = "SAVED Come to a rational conclusion...";

        }

        public void NewTR()
        {
            thoughtRecord = new ThoughtRecord();
            OnPropertyChanged(null);
        }
    }
}
