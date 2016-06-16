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
    /// <summary>
    /// A class holding data/objects that are accessed by all pages which is to be passed as the navigation parameter
    /// on every page navigation
    /// </summary>
    public class INavigationParameterModel
    {
        object navigationParameter { get; set; }
        IDatabaseService DatabaseService { get; }
        IStringResourceService StringResourceService { get; }
        MainPage CurrentMain { get; set; }
        VoiceCommandActivatedEventArgs VoiceCommandActivatedEventArgs { get; set; }
        LicenseInformation LicenseInformation { get;}
    }
}
