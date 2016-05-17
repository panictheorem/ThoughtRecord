using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.ViewModels;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.Services
{
    public static class InformationService
    {
        public static void GetInformationPageText(InformationModel model)
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("InformationText");
        }
    }
}
