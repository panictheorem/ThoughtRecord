using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;
using ThoughtRecordApp.DAL.Concrete;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.Infrastructure.Interfaces;
using ThoughtRecordApp.Services;
using ThoughtRecordApp.ViewModels.Infrastructure;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.ViewModels
{
    /// <summary>
    /// Holds data to be displayed with ThoughtRecordListPage. 
    /// </summary>
    public class ThoughtRecordListModel : BindableBase
    {
        public static string Title { get; private set; }
        public ObservableCollection<Situation> Situations { get; set; }
        private IDatabaseService database;
        private IStringResourceService stringLoader;

        public ThoughtRecordListModel(IDatabaseService db)
        {
            stringLoader = new StringResourceService("PageTitles");
            Title = Title = ResourceLoader.GetForCurrentView("PageTitles").GetString("MyThoughtRecordsTitle");
            database = db;
            Situations = new ObservableCollection<Situation>();
        }
        /// <summary>
        /// Initialize method must be called after instantiating to populate the ThoughtRecord list
        /// </summary>
        public async Task Initialize()
        {
            //Records retrieved and ordered by datetime from newest to oldest.
            var situations = (await database.Situations.GetAllAsync())
                          .OrderByDescending(s => s.DateTime)
                          .ToList();
            //Add the records to the observable collection.
            foreach (var s in situations)
            {
                Situations.Add(s);
            }
        }
    }
}
