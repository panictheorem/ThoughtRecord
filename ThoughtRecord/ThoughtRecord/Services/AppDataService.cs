using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;
using Windows.UI.Xaml;

namespace ThoughtRecordApp.Services
{
    public static class AppDataService
    {
        public static IDatabaseService GetDatabase(Application app)
        {
            return (Application.Current as App).Database;
        }

    }
}
