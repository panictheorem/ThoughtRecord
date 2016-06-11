using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Store;

namespace ThoughtRecordApp.Pages.Infrastructure.Interfaces
{
    public class INavigationParameterModel
    {
        object navigationParameter { get; set; }
        IDatabaseService DatabaseService { get; }
        MainPage CurrentMain { get; set; }
        VoiceCommandActivatedEventArgs VoiceCommandActivatedEventArgs { get; set; }
        LicenseInformation LicenseInformation { get;}
    }
}
