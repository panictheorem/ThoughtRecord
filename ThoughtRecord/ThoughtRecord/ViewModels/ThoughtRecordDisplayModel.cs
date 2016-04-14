using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Models;

namespace ThoughtRecordApp.ViewModels
{
    public class ThoughtRecordDisplayModel
    {
        public ThoughtRecord ThoughtRecord { get; set; }
        public Settings Settings { get; set; }

        public ThoughtRecordDisplayModel()
        {
            ThoughtRecord = new ThoughtRecord();
            Settings = new Settings();
        }
    }
}
