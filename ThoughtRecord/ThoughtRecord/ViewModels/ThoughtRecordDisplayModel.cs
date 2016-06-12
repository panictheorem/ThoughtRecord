using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ThoughtRecordApp.DAL.Abstract;
using ThoughtRecordApp.DAL.Concrete;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.Services;
using ThoughtRecordApp.ViewModels.Infrastructure;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordDisplayModel : BindableBase
    {
        public static string Title { get; private set; }
        private int thoughtRecordId;
        private IDatabaseService database;
        private List<ThoughtRecord> thoughtRecords;
        private int currentIndex;
        private bool commandsEnabled;
        public ThoughtRecordSectionTitleModel SectionTitles { get; private set; }
        public delegate void ThoughtRecordEvent(object sender, EventArgs args);
        public event ThoughtRecordEvent OnThoughtRecordEditRequested;
        public event ThoughtRecordEvent OnThoughtRecordDeleteRequested;
        public event ThoughtRecordEvent OnThoughtRecordDeleted;
        public event ThoughtRecordEvent OnThoughtRecordChanged;

        public ThoughtRecordDisplayModel(IDatabaseService db)
        {
            database = db;
            Title = ResourceLoader.GetForCurrentView("PageTitles").GetString("MyThoughtRecordsTitle");
            SectionTitles = ThoughtRecordService.GetTitleModel();
        }
        public async Task InitializeModel(int thoughtRecordId)
        {
            thoughtRecords = (await database.ThoughtRecords.GetAllAsync()).OrderByDescending(tr => tr.Situation.DateTime).ToList();
            //if id is 0, such as when invoked by Cortana command, get latest record
            if (ThoughtRecordId == 0)
            {
                ThoughtRecord = thoughtRecords.FirstOrDefault();
            }
            else
            {
                ThoughtRecord = thoughtRecords.Where(t => t.ThoughtRecordId == thoughtRecordId).FirstOrDefault();
            }
            currentIndex = thoughtRecords.IndexOf(ThoughtRecord);
            commandsEnabled = true;
            this.thoughtRecordId = ThoughtRecord.ThoughtRecordId;
            RaiseCanExecuteChangedForAll();
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
                OnPropertyChanged();
            }
        }

        public int ThoughtRecordId
        {
            get
            {
                return thoughtRecordId;
            }
            private set
            {
                thoughtRecordId = value;
            }
        }
        private RelayCommand requestDeleteThoughtRecord;
        public ICommand RequestDelete
        {
            get
            {
                if (requestDeleteThoughtRecord == null)
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
                    deleteThoughtRecord = new RelayCommand(DeleteThoughtRecord, CommandsAreEnabled);
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
                    editThoughtRecord = new RelayCommand(NotifyEditRequested, CommandsAreEnabled);
                }
                return editThoughtRecord;
            }
        }
        private RelayCommand selectNextNewerThoughtRecord;
        public ICommand GetNewer
        {
            get
            {
                if (selectNextNewerThoughtRecord == null)
                {
                    selectNextNewerThoughtRecord = new RelayCommand(SelectNextNewerThoughtRecord, CanSelectNewerThoughtRecord);
                }
                return selectNextNewerThoughtRecord;
            }
        }
        private RelayCommand selectNextOlderThoughtRecord;
        public ICommand GetOlder
        {
            get
            {
                if (selectNextOlderThoughtRecord == null)
                {
                    selectNextOlderThoughtRecord = new RelayCommand(SelectNextOlderThoughtRecord, CanSelectOlderThoughtRecord);
                }
                return selectNextOlderThoughtRecord;
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
        private async void DeleteThoughtRecord()
        {
            commandsEnabled = false;
            await database.ThoughtRecords.DeleteAsync(ThoughtRecord.ThoughtRecordId);
            OnThoughtRecordDeleted?.Invoke(this, new EventArgs());
            commandsEnabled = false;
        }

        private void SelectNextNewerThoughtRecord()
        {
            ThoughtRecord = thoughtRecords[--currentIndex];
            selectNextNewerThoughtRecord.RaiseCanExecuteChanged();
            selectNextOlderThoughtRecord.RaiseCanExecuteChanged();
            thoughtRecordId = ThoughtRecord.ThoughtRecordId;
            OnThoughtRecordChanged?.Invoke(this, new EventArgs());
        }

        private void SelectNextOlderThoughtRecord()
        {
            ThoughtRecord = thoughtRecords[++currentIndex];
            selectNextNewerThoughtRecord.RaiseCanExecuteChanged();
            selectNextOlderThoughtRecord.RaiseCanExecuteChanged();
            thoughtRecordId = ThoughtRecord.ThoughtRecordId;
            OnThoughtRecordChanged?.Invoke(this, new EventArgs());
        }

        private bool CanSelectNewerThoughtRecord()
        {
            return commandsEnabled && thoughtRecords != null && currentIndex > 0;
        }
        private bool CanSelectOlderThoughtRecord()
        {
            return commandsEnabled && thoughtRecords != null && currentIndex < thoughtRecords.Count - 1;
        }

        private bool CommandsAreEnabled()
        {
            return commandsEnabled;
        }

        private void RaiseCanExecuteChangedForAll()
        {
            (GetOlder as RelayCommand).RaiseCanExecuteChanged();
            (GetNewer as RelayCommand).RaiseCanExecuteChanged();
            (Delete as RelayCommand).RaiseCanExecuteChanged();
            (Edit as RelayCommand).RaiseCanExecuteChanged();
        }
    }
}
