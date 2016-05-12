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
    /// <summary>
    /// Holds data to be displayed with ThoughtRecordListPage. 
    /// </summary>
    public class ThoughtRecordListModel : BindableBase
    {
        public const string Title = "My Thought Records";
        public ObservableCollection<ThoughtRecord> ThoughtRecords { get; set; }
        private IDatabaseService database;

        public ThoughtRecordListModel(IDatabaseService db)
        {
            database = db;
            ThoughtRecords = new ObservableCollection<ThoughtRecord>();
        }

        public async Task Initialize()
        {
            var records = (await database.ThoughtRecords.GetAllAsync())
                          .OrderByDescending(tr => tr.Situation.DateTime.Date)
                          .ToList();
            records.OrderByDescending(tr => tr.Situation.DateTime.Date);
            foreach (var r in records)
            {
                ThoughtRecords.Add(r);
            }
        }
    }
}
