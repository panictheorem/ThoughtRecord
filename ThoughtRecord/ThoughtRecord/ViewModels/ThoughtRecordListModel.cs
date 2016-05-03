using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Concrete;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.ViewModels.Infrastructure;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordListModel : BindableBase
    {
        public ObservableCollection<ThoughtRecord> ThoughtRecords { get; set; }
        private DatabaseService service = new DatabaseService();

        public ThoughtRecordListModel()
        {
            ThoughtRecords = new ObservableCollection<ThoughtRecord>();
        }

        public async Task Initialize()
        {
            var records = await service.ThoughtRecords.GetAllAsync();
            foreach (var r in records)
            {
                ThoughtRecords.Add(r);
            }
            ThoughtRecords.OrderBy(tr => tr.Situation.DateTime);
        }
    }
}
