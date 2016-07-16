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
using System.ComponentModel;
using Windows.ApplicationModel.Resources;
using ThoughtRecordApp.Infrastructure.Interfaces;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordEditModel : BindableBase, IDisposable
    {
        //Title for when creating a new record.
        private readonly string newThoughtRecordTitle = "New Thought Record";
        //Title for when editing a record.
        private readonly string editThoughtRecordTitle = "Edit Thought Record";
        public string Title { get; private set; }
        //The text displayed in textboxes when a new thought record is created
        public List<string> DefaultInputText { get; private set; }
        private IDatabaseService database;
        private ThoughtRecordService thoughtRecordService;
        private IStringResourceService stringLoader;
        public ThoughtRecordSectionTitleModel SectionTitles { get; private set; }
        public delegate void ThoughtRecordEditEvent(object sender, EventArgs args);
        public event ThoughtRecordEditEvent OnThoughtRecordSaving;
        public event ThoughtRecordEditEvent OnThoughtRecordSaved;
        public event ThoughtRecordEditEvent OnNewThoughtRecordOverwriteRisk;
        public event ThoughtRecordEditEvent OnNewThoughtRecordCreated;
        private bool isCurrentDataSaved = true;
        private bool commandsEnabled;

        public ThoughtRecordEditModel(IDatabaseService db)
        {
            stringLoader = new StringResourceService("PageTitles");
            newThoughtRecordTitle = stringLoader.GetString("NewThoughtRecordPageTitle");
            editThoughtRecordTitle = stringLoader.GetString("EditThoughtRecordPageTitle");
            thoughtRecordService = new ThoughtRecordService();
            SectionTitles = thoughtRecordService.GetTitleModel();
            database = db;
            CreateNewThoughtRecord();
            commandsEnabled = true;
            IsCurrentDataSaved = true;
        }

        public ThoughtRecordEditModel(int thoughtRecordId, IDatabaseService db)
        {
            stringLoader = new StringResourceService("PageTitles");
            newThoughtRecordTitle = stringLoader.GetString("NewThoughtRecordPageTitle");
            editThoughtRecordTitle = stringLoader.GetString("EditThoughtRecordPageTitle");
            Title = editThoughtRecordTitle;
            thoughtRecordService = new ThoughtRecordService();
            SectionTitles = thoughtRecordService.GetTitleModel();
            database = db;
            InitializeThoughtRecord(thoughtRecordId);
            DefaultInputText = thoughtRecordService.GetDefaultInputText();
            commandsEnabled = true;
        }

        public void CreateNewThoughtRecord()
        {
            Title = newThoughtRecordTitle;
            thoughtRecord = new ThoughtRecord();
            thoughtRecord.Situation = new Situation();
            DateTime now = DateTime.Now;
            thoughtRecord.Situation.DateTime = new DateTime(
                year: now.Year,  
                month: now.Month,
                day: now.Day,
                hour: 12,
                minute: 1,
                second: 1,
                millisecond: 1,
                kind: DateTimeKind.Utc);
            thoughtRecord.Emotions = new List<Emotion>();
            DefaultInputText = thoughtRecordService.GetDefaultInputText();
            thoughtRecordService.PopulateWithDefaultValues(thoughtRecord);
            observableEmotions = new ObservableCollection<Emotion>(thoughtRecord.Emotions);
            RegisterForEmotionChangeEvents(observableEmotions);
            observableEmotions.CollectionChanged += UpdateModelEmotionCollection;
            OnPropertyChanged(string.Empty);
            IsCurrentDataSaved = true;
            OnNewThoughtRecordCreated?.Invoke(this, new EventArgs());
        }

        private void RegisterForEmotionChangeEvents(ObservableCollection<Emotion> emotions)
        {
            foreach (var emotion in emotions)
            {
                emotion.PropertyChanged += Emotion_PropertyChanged;
            }
        }

        private void DeregisterForEmotionChangeEvents(ObservableCollection<Emotion> emotions)
        {
            foreach (var emotion in emotions)
            {
                if (emotion != null)
                {
                    emotion.PropertyChanged -= Emotion_PropertyChanged;
                }
            }
        }

        private async void InitializeThoughtRecord(int thoughtRecordId)
        {
            ThoughtRecord = await database.ThoughtRecords.GetByIdAsync(thoughtRecordId);
            Emotions = new ObservableCollection<Emotion>(thoughtRecord.Emotions);
            observableEmotions.CollectionChanged += UpdateModelEmotionCollection;
            RegisterForEmotionChangeEvents(observableEmotions);
            IsCurrentDataSaved = true;
        }

        public bool IsCurrentDataSaved
        {
            get { return isCurrentDataSaved; }
            set
            {
                isCurrentDataSaved = value;
                ((RelayCommand)Save).RaiseCanExecuteChanged();
            }
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
                IsCurrentDataSaved = false;
            }
        }

        public DateTime? SituationDateTime
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
                        if (value != null)
                        {
                            thoughtRecord.Situation.DateTime = (DateTime)value;
                        }
                        OnPropertyChanged();
                        IsCurrentDataSaved = false;
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
                    IsCurrentDataSaved = false;
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
                    IsCurrentDataSaved = false;
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
                    IsCurrentDataSaved = false;
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
                    IsCurrentDataSaved = false;
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
                    IsCurrentDataSaved = false;
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
                    if (Emotions != value)
                    {
                        observableEmotions = value;
                        OnPropertyChanged();
                        IsCurrentDataSaved = false;
                    }
                }
            }
        }

        /// <summary>
        /// Method to update the model's emotion list when the observable collection changes.
        /// This keeps the two lists syncronized.
        /// </summary>
        private void UpdateModelEmotionCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsCurrentDataSaved = false;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    thoughtRecord.Emotions.AddRange(e.NewItems.OfType<Emotion>());
                    foreach (Emotion emotion in e.NewItems.OfType<Emotion>())
                    {
                        emotion.PropertyChanged += Emotion_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    thoughtRecord.Emotions.RemoveAll(emotion => e.OldItems.Contains(emotion));
                    foreach (Emotion emotion in e.OldItems.OfType<Emotion>())
                    {
                        if (emotion != null)
                        {
                            emotion.PropertyChanged -= Emotion_PropertyChanged;
                        }
                    }
                    break;
            }
        }

        private void Emotion_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsCurrentDataSaved = false;
        }

        private RelayCommand saveThoughtRecord;
        public ICommand Save
        {
            get
            {
                if (saveThoughtRecord == null)
                {
                    saveThoughtRecord = new RelayCommand(SaveThoughtRecord, SaveEnabled);
                }
                return saveThoughtRecord;
            }
        }
        private bool CommandsEnabled()
        {
            return commandsEnabled;
        }
        private bool SaveEnabled()
        {
            return CommandsEnabled() && !isCurrentDataSaved;
        }
        public async void SaveThoughtRecord()
        {
            OnThoughtRecordSaving?.Invoke(this, new EventArgs());
            commandsEnabled = false;
            RaiseCanExecuteChangedAll();
            await database.ThoughtRecords.InsertOrUpdateAsync(thoughtRecord);
            commandsEnabled = true;
            RaiseCanExecuteChangedAll();
            if (!IsCurrentDataSaved)
            {
                IsCurrentDataSaved = true;
            }
            OnThoughtRecordSaved?.Invoke(this, new EventArgs());
        }

        private RelayCommand requestNewThoughtRecord;
        public ICommand RequestNew
        {
            get
            {
                if (requestNewThoughtRecord == null)
                {
                    requestNewThoughtRecord = new RelayCommand(InitiateNewThoughtRecord, CommandsEnabled);
                }
                return requestNewThoughtRecord;
            }
        }

        public void InitiateNewThoughtRecord()
        {
            if (isCurrentDataSaved)
            {
                CreateNewThoughtRecord();
            }
            else
            {
                //Alert listeners if proceeding would cause them to lose their current progress.
                OnNewThoughtRecordOverwriteRisk?.Invoke(this, new EventArgs());
            }
        }

        private void RaiseCanExecuteChangedAll()
        {
            ((RelayCommand)Save).RaiseCanExecuteChanged();
            ((RelayCommand)RequestNew).RaiseCanExecuteChanged();
        }

        public void Dispose()
        {
            //Deregister event handlers
            observableEmotions.CollectionChanged -= UpdateModelEmotionCollection;
            foreach (Emotion emotion in Emotions)
            {
                if (emotion != null)
                {
                    emotion.PropertyChanged -= Emotion_PropertyChanged;
                }
            }
        }
    }
}
