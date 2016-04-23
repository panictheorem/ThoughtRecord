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
                if (thoughtRecord != null)
                {
                    return thoughtRecord.Situation.DateTime;
                }
                return new DateTime();
            }
            set
            {
                if (thoughtRecord != null)
                {
                    if(thoughtRecord.Situation.DateTime != value)
                    {
                        thoughtRecord.Situation.DateTime = value;
                        OnPropertyChanged();
                    }
                }
            }
        }
        public string SituationDescription
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.Situation.Description;
                }
                return string.Empty;
            }
            set
            {
                if (thoughtRecord != null)
                {
                    this.thoughtRecord.Situation.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AutomaticThoughts
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.AutomaticThoughts;
                }
                return string.Empty;
            }
            set
            {
                if (thoughtRecord != null)
                {
                    thoughtRecord.AutomaticThoughts = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SupportingEvidence
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.SupportingEvidence;
                }
                return string.Empty;
            }
            set
            {
                if (thoughtRecord != null)
                {
                    thoughtRecord.SupportingEvidence = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ContradictingEvidence
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.ContradictingEvidence;
                }
                return string.Empty;
            }
            set
            {
                if (thoughtRecord != null)
                {
                    thoughtRecord.ContradictingEvidence = value;
                    OnPropertyChanged();
                }
            }
        }
        public string RationalAssessment
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.RationalAssessment;
                }
                return string.Empty;
            }
            set
            {
                if (thoughtRecord != null)
                {
                    thoughtRecord.RationalAssessment = value;
                    OnPropertyChanged();
                }
            }
        }
        public DeeplyObservableCollection<Emotion> Emotions
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.Emotions;
                }
                return new DeeplyObservableCollection<Emotion>();
            }
            set
            {
                if (thoughtRecord != null)
                {
                    thoughtRecord.Emotions = value;
                }
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
