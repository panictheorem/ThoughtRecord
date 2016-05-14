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
        /// <summary>
        /// Initialize method must be called after instantiating to populate the ThoughtRecord list
        /// </summary>
        public async Task Initialize()
        {
            //Records retrieved and ordered by datetime from newest to oldest.
            var records = (await database.ThoughtRecords.GetAllAsync())
                          .OrderByDescending(tr => tr.Situation.DateTime)
                          .ToList();
            //Add the records to the observable collection.
            foreach (var r in records)
            {
                ThoughtRecords.Add(r);
            }
        }
    }
}
