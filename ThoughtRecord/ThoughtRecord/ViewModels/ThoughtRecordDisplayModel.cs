using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ThoughtRecordApp.DAL.Concrete;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.ViewModels.Infrastructure;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordDisplayModel : BindableBase
    {
        private DatabaseService database;
        public delegate void ThoughtRecordEvent(object sender, EventArgs args);
        public event ThoughtRecordEvent OnThoughtRecordEditRequested;
        public event ThoughtRecordEvent OnThoughtRecordDeleteRequested;
        public event ThoughtRecordEvent OnThoughtRecordDeleted;

        public ThoughtRecordDisplayModel(int thoughtRecordId)
        {
            database = new DatabaseService();
            ThoughtRecord = database.ThoughtRecords.GetById(thoughtRecordId);
        }

        private ThoughtRecord thoughtRecord;
        public ThoughtRecord ThoughtRecord
        {
            get
            {
                return thoughtRecord;
            }
            set
            {
                thoughtRecord = value;
                OnPropertyChanged(null);
            }
        }
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
                    if (thoughtRecord.Situation.DateTime != value)
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
        public List<Emotion> Emotions
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return thoughtRecord.Emotions;
                }
                return new List<Emotion>();
            }
            set
            {
                if (thoughtRecord != null)
                {
                    if (thoughtRecord.Emotions != value)
                    {
                        thoughtRecord.Emotions = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private RelayCommand requestDeleteThoughtRecord;
        public ICommand RequestDelete
        {
            get
            {
                if(requestDeleteThoughtRecord == null)
                {
                    requestDeleteThoughtRecord = new RelayCommand(InitiateDeleteThoughtRecord);
                }
                return requestDeleteThoughtRecord;
            }
        }
        private RelayCommand deleteThoughtRecord;
        public ICommand Delete
        {
            get
            {
                if (deleteThoughtRecord == null)
                {
                    deleteThoughtRecord = new RelayCommand(DeleteThoughtRecord);
                }
                return deleteThoughtRecord;
            }
        }
        private RelayCommand editThoughtRecord;
        public ICommand Edit
        {
            get
            {
                if (editThoughtRecord == null)
                {
                    editThoughtRecord = new RelayCommand(NotifyEditRequested);
                }
                return editThoughtRecord;
            }
        }
        private void NotifyEditRequested()
        {
            OnThoughtRecordEditRequested?.Invoke(this, new EventArgs());
        }

        private void InitiateDeleteThoughtRecord()
        {
            OnThoughtRecordDeleteRequested?.Invoke(this, new EventArgs());
        }
        private void DeleteThoughtRecord()
        {
            database.ThoughtRecords.Delete(ThoughtRecord.ThoughtRecordId);
            OnThoughtRecordDeleted?.Invoke(this, new EventArgs());
        }

    }
}
