using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;
using ThoughtRecordApp.Pages.Infrastructure.Interfaces;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Store;

namespace ThoughtRecordApp.Pages.Infrastructure.Implementations
{
    public class NavigationParameterModel : INavigationParameterModel
    {
        public object navigationParameter { get; set; }
        public IDatabaseService DatabaseService { get; }
        public MainPage CurrentMain { get; }
        public VoiceCommandActivatedEventArgs VoiceCommandActivatedEventArgs { get; }
        public LicenseInformation LicenseInformation { get; }

        public NavigationParameterModel(IDatabaseService databaseService, LicenseInformation licenseInformation)
        {
            DatabaseService = databaseService; 
            LicenseInformation = licenseInformation;
        }

        public NavigationParameterModel(IDatabaseService databaseService, LicenseInformation licenseInformation, VoiceCommandActivatedEventArgs voiceCommandArgs)
        {
            DatabaseService = databaseService;
            LicenseInformation = licenseInformation;
            VoiceCommandActivatedEventArgs = voiceCommandArgs;
        }
    }
}
