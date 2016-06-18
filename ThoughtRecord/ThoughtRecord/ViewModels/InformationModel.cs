using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using ThoughtRecordApp.Infrastructure.Interfaces;
using ThoughtRecordApp.Services;
namespace ThoughtRecordApp.ViewModels
{
    public class InformationModel
    {
        private IStringResourceService stringLoader;

        public static string Title { get; private set; }

        public string InstructionsTitle { get; private set; }
        public string WhatIsThoughtRecordSectionTitle { get; private set; }
        public string WhatIsThoughtRecordSectionContent { get; private set; }
        public string StepsSectionTitle { get; private set; }
        public string StepsSectionContent { get; private set; }
        public string Step1 { get; private set; }
        public string Step2 { get; private set; }
        public string Step3 { get; private set; }
        public string Step4 { get; private set; }
        public string Step5 { get; private set; }
        public string Step6 { get; private set; }

        public string TipsTitle { get; private set; }
        public string Tip1 { get; private set; }
        public string Tip2 { get; private set; }
        public string Tip3 { get; private set; }

        public string VoiceCommandsTitle { get; private set; }
        public string VoiceCommandsIntro { get; private set; }
        public string OpenCommandsSectionTitle { get; private set; }
        public string OpenCommandsSectionIntro { get; private set; }

        public string OpenCommandDescription1 { get; private set; }
        public string OpenCommandDescription2 { get; private set; }
        public string OpenCommandDescription3 { get; private set; }
        public string OpenCommandDescription4 { get; private set; }

        public string OpenCommand1 { get; private set; }
        public string OpenCommand2 { get; private set; }
        public string OpenCommand3 { get; private set; }
        public string OpenCommand4 { get; private set; }


        public string AboutTitle { get; private set; }
        public string AboutContent { get; private set; }
        public string BroughtToYouByText { get; private set; }
        public string ContactMeLinkText { get; private set; }
        public string ContactMeContent { get; private set; }
        public string DonateContent { get; private set; }
        public string DonateButtonText { get; set; }
        public string ImageSourceText { get; set; }
        public string ImageSourceLinkText { get; set; }

        public InformationModel()
        {
            stringLoader = new StringResourceService("PageTitles");
            Title = stringLoader.GetString("InformationPageTitle");
            InitializeContent();
        }

        private void InitializeContent()
        {
            stringLoader.SetFile("InformationText");
            InstructionsTitle = stringLoader.GetString("InstructionsTitle");
            WhatIsThoughtRecordSectionTitle = stringLoader.GetString("WhatIsThoughtRecordSectionTitle");
            WhatIsThoughtRecordSectionContent = stringLoader.GetString("WhatIsThoughtRecordSectionContent");
            StepsSectionTitle = stringLoader.GetString("StepsSectionTitle");
            StepsSectionContent = stringLoader.GetString("StepsSectionContent");
            Step1 = stringLoader.GetString("Step1");
            Step2 = stringLoader.GetString("Step2");
            Step3 = stringLoader.GetString("Step3");
            Step4 = stringLoader.GetString("Step4");
            Step5 = stringLoader.GetString("Step5");
            Step6 = stringLoader.GetString("Step6");

            TipsTitle = stringLoader.GetString("TipsTitle");
            Tip1 = stringLoader.GetString("Tip1");
            Tip2= stringLoader.GetString("Tip2");
            Tip3 = stringLoader.GetString("Tip3");

            VoiceCommandsTitle = stringLoader.GetString("VoiceCommandsTitle");
            VoiceCommandsIntro = stringLoader.GetString("VoiceCommandsIntro");
            OpenCommandsSectionTitle = stringLoader.GetString("OpenCommandsSectionTitle");
            OpenCommandsSectionIntro = stringLoader.GetString("OpenCommandsSectionIntro");

            OpenCommandDescription1 = stringLoader.GetString("OpenCommandDescription1");
            OpenCommandDescription2 = stringLoader.GetString("OpenCommandDescription2");
            OpenCommandDescription3 = stringLoader.GetString("OpenCommandDescription3");
            OpenCommandDescription4 = stringLoader.GetString("OpenCommandDescription4");

            OpenCommand1 = stringLoader.GetString("OpenCommand1");
            OpenCommand2 = stringLoader.GetString("OpenCommand2");
            OpenCommand3 = stringLoader.GetString("OpenCommand3");
            OpenCommand4 = stringLoader.GetString("OpenCommand4");

            AboutTitle = stringLoader.GetString("AboutTitle");
            AboutContent = stringLoader.GetString("AboutContent");
            BroughtToYouByText = stringLoader.GetString("BroughtToYouByText");
            ContactMeLinkText = stringLoader.GetString("ContactMeLinkText");
            ContactMeContent = stringLoader.GetString("ContactMeContent");
            DonateContent = stringLoader.GetString("DonateContent");
            DonateButtonText = stringLoader.GetString("DonateButtonText");
            ImageSourceText = stringLoader.GetString("ImageSourceText");
            ImageSourceLinkText = stringLoader.GetString("ImageSourceLinkText");
        }
    }
}
