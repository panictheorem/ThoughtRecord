using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Pages.Infrastructure.Interfaces;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    /// <summary>
    /// Facade class to encapsulate the ResourceLoader used to get strings from 
    /// resource files
    /// </summary>
    public class StringResourceService : IStringResourceService
    {
        private ResourceLoader resourceLoader;

        public void SetFile(string fileName)
        {
            ResourceLoader.GetForCurrentView(fileName);
        }

        public string GetString(string key)
        {
            if(resourceLoader != null)
            {
                return resourceLoader.GetString(key);
            }
            else
            {
                return null;
            }
        }

        public string GetString(string fileName, string key)
        {
            SetFile(fileName);
            return resourceLoader.GetString(key);
        }

    }
}
