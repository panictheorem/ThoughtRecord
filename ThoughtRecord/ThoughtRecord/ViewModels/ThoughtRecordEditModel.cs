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
using System.Windows.Input;
using ThoughtRecordApp.DAL.Abstract;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordEditModel : BindableBase
    {
        public delegate void ThoughtRecordEditEvent(object sender, EventArgs args);
        public event ThoughtRecordEditEvent OnThoughtRecordSaving;
        public event ThoughtRecordEditEvent OnThoughtRecordSaved;
        public event ThoughtRecordEditEvent OnNewThoughtRecordOverwriteRisk;
        private IDatabaseService database;
        private bool isCurrentDataSaved = true;
        private bool commandsEnabled;

        public ThoughtRecordEditModel(IDatabaseService db)
        {
            database = db;
            CreateNewThoughtRecord();
            commandsEnabled = true;
        }

        public ThoughtRecordEditModel(int thoughtRecordId, IDatabaseService db)
        {
            database = db;
            InitializeThoughtRecord(thoughtRecordId);
            DefaultInputText = ThoughtRecordService.GetDefaultInputText();
            commandsEnabled = true;
        }

        public void CreateNewThoughtRecord()
        {
            thoughtRecord = new ThoughtRecord();
            thoughtRecord.Situation = new Situation();
            thoughtRecord.Situation.DateTime = DateTime.Now;
            thoughtRecord.Emotions = new List<Emotion>();
            DefaultInputText = ThoughtRecordService.GetDefaultInputText();
            ThoughtRecordService.PopulateWithDefaultValues(thoughtRecord);
            observableEmotions = new ObservableCollection<Emotion>(thoughtRecord.Emotions);
            observableEmotions.CollectionChanged += UpdateModelEmotionCollection;
            OnPropertyChanged(string.Empty);
        }
        private async void InitializeThoughtRecord(int thoughtRecordId)
        {
            ThoughtRecord = await database.ThoughtRecords.GetByIdAsync(thoughtRecordId);
            Emotions = new DeeplyObservableCollection<Emotion>(thoughtRecord.Emotions);
            observableEmotions.CollectionChanged += UpdateModelEmotionCollection;

        }

        public List<string> DefaultInputText { get; private set; }

        public bool IsCurrentDataSaved
        {
            get { return isCurrentDataSaved; }
            set { isCurrentDataSaved = value; }
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
                OnPropertyChanged(string.Empty);
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
        private ObservableCollection<Emotion> observableEmotions;
        public ObservableCollection<Emotion> Emotions
        {
            get
            {
                if (thoughtRecord != null)
                {
                    return observableEmotions;
                }
                return new ObservableCollection<Emotion>();
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

        private RelayCommand saveThoughtRecord;
        public ICommand Save
        {
            get
            {
                if(saveThoughtRecord == null)
                {
                    saveThoughtRecord = new RelayCommand(SaveThoughtRecord, CanSaveThoughtRecord);
                }
                return saveThoughtRecord;
            }
        }
        private bool canSaveThoughtRecord = true;
        public bool CanSaveThoughtRecord()
        {
            return canSaveThoughtRecord;
        }
        public async void SaveThoughtRecord()
        {
            OnThoughtRecordSaving?.Invoke(this, new EventArgs());
            canSaveThoughtRecord = false;
            saveThoughtRecord.RaiseCanExecuteChanged();
            await database.ThoughtRecords.InsertOrUpdateAsync(thoughtRecord);
            canSaveThoughtRecord = true;
            saveThoughtRecord.RaiseCanExecuteChanged();
            if(!IsCurrentDataSaved)
            {
                IsCurrentDataSaved = true;
            }
            OnThoughtRecordSaved?.Invoke(this, new EventArgs());
        }

        private RelayCommand newThoughtRecord;
        public ICommand RequestNew
        {
            get
            {
                if (newThoughtRecord == null)
                {
                    newThoughtRecord = new RelayCommand(InitiateNewThoughtRecord, CommandsEnabled);
                }
                return newThoughtRecord;
            }
        }

        public bool CommandsEnabled()
        {
            return commandsEnabled;
        }
        public void InitiateNewThoughtRecord()
        {
            if(isCurrentDataSaved)
            {
                CreateNewThoughtRecord();
            }
            else
            {
                OnNewThoughtRecordOverwriteRisk?.Invoke(this, new EventArgs());
            }
        }
    }
}
