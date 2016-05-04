using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;
using ThoughtRecordApp.DAL.Concrete;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.ViewModels.Infrastructure;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordListModel : BindableBase
    {
        public ObservableCollection<ThoughtRecord> ThoughtRecords { get; set; }
        private IDatabaseService database;

        public ThoughtRecordListModel(IDatabaseService db)
        {
            database = db;
            ThoughtRecords = new ObservableCollection<ThoughtRecord>();
        }

        public async Task Initialize()
        {
            var records = await database.ThoughtRecords.GetAllAsync();
            foreach (var r in records)
            {
                ThoughtRecords.Add(r);
            }
            ThoughtRecords.OrderBy(tr => tr.Situation.DateTime);
        }
    }
}
