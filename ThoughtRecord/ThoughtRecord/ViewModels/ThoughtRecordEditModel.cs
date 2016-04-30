using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Services;
using ThoughtRecordApp.ViewModels.Infrastructure;
using ThoughtRecordApp.DAL.Models;
using System.Collections.Specialized;
using ThoughtRecordApp.DAL.Concrete;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Popups;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordEditModel : BindableBase
    {
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
        private DeeplyObservableCollection<Emotion> observableEmotions;
        public DeeplyObservableCollection<Emotion> Emotions
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return observableEmotions;
                }
                return new DeeplyObservableCollection<Emotion>();
            }
            set
            {
                if (thoughtRecord != null)
                {
                    if(Emotions != value)
                    {
                        observableEmotions = value;
                        OnPropertyChanged();
                    }
                }
            }
        }
        public List<string> DefaultInputText { get; }
        public Configuration Settings { get; }

        public ThoughtRecordEditModel()
        {
            thoughtRecord = new ThoughtRecord();
            thoughtRecord.Situation = new Situation();
            thoughtRecord.Situation.DateTime = DateTime.Now;
            thoughtRecord.Emotions = new List<Emotion>();
            DefaultInputText = ThoughtRecordService.GetDefaultInputText();
            Settings = new Configuration();
            ThoughtRecordService.PopulateWithDefaultValues(thoughtRecord);
            observableEmotions = new DeeplyObservableCollection<Emotion>(thoughtRecord.Emotions);
            observableEmotions.CollectionChanged += UpdateModelEmotionCollection;
        }

        private void UpdateModelEmotionCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    thoughtRecord.Emotions.AddRange(e.NewItems.OfType<Emotion>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    thoughtRecord.Emotions.RemoveAll(emotion => e.OldItems.Contains(emotion));
                    break;
            }
        }

        public ThoughtRecordEditModel(int thoughtRecordId)
        {
            InitializeThoughtRecord(thoughtRecordId);
        }
        private async void InitializeThoughtRecord(int thoughtRecordId)
        {
            var db = new DatabaseService();
            thoughtRecord = await db.ThoughtRecords.GetByIdAsync(thoughtRecordId);
            Emotions = new DeeplyObservableCollection<Emotion>(thoughtRecord.Emotions);
            observableEmotions.CollectionChanged += UpdateModelEmotionCollection;
        }
        public async Task Save()
        {
            var db = new DatabaseService();
            await db.ThoughtRecords.InsertOrUpdateAsync(thoughtRecord);
        }
    }
}
