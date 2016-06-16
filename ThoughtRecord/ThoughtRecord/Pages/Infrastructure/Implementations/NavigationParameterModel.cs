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
        public object NavigationParameter { get; set; } //The main item of data passed to a page
        public IDatabaseService DatabaseService { get; }
        public IStringResourceService StringResourceService { get; }
        public MainPage CurrentMain { get; }
        public VoiceCommandActivatedEventArgs VoiceCommandActivatedEventArgs { get; }
        public LicenseInformation LicenseInformation { get; }

        /// <summary>
        /// These constructors are called by the App class to initialize
        /// </summary>

        public NavigationParameterModel(IDatabaseService databaseService, 
                                        LicenseInformation licenseInformation,
                                        IStringResourceService stringResourceService)
        {
            DatabaseService = databaseService; 
            LicenseInformation = licenseInformation;
            StringResourceService = stringResourceService;
        }

        public NavigationParameterModel(IDatabaseService databaseService, 
                                        LicenseInformation licenseInformation,
                                        IStringResourceService stringResourceService,
                                        VoiceCommandActivatedEventArgs voiceCommandArgs)
        {
            DatabaseService = databaseService;
            LicenseInformation = licenseInformation;
            StringResourceService = stringResourceService;
            VoiceCommandActivatedEventArgs = voiceCommandArgs;
        }
    }
}
