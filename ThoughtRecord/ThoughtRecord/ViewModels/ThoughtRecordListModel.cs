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

        public ThoughtRecordListModel()
        {
            ThoughtRecords = new ObservableCollection<ThoughtRecord>();
            DatabaseService service = new DatabaseService();
            var tr = service.ThoughtRecords.GetAll();
            foreach(var t in tr)
            {
                ThoughtRecords.Add(t);
            }
        }
    }
}
