using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace ThoughtRecordApp.ViewModels
{
    public class InformationModel
    {
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
            Title = ResourceLoader.GetForCurrentView("PageTitles").GetString("InformationPageTitle");
            InitializeContent();
        }

        private void InitializeContent()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("InformationText");
            InstructionsTitle = resources.GetString("InstructionsTitle");
            WhatIsThoughtRecordSectionTitle = resources.GetString("WhatIsThoughtRecordSectionTitle");
            WhatIsThoughtRecordSectionContent = resources.GetString("WhatIsThoughtRecordSectionContent");
            StepsSectionTitle = resources.GetString("StepsSectionTitle");
            StepsSectionContent = resources.GetString("StepsSectionContent");
            Step1 = resources.GetString("Step1");
            Step2 = resources.GetString("Step2");
            Step3 = resources.GetString("Step3");
            Step4 = resources.GetString("Step4");
            Step5 = resources.GetString("Step5");
            Step6 = resources.GetString("Step6");

            TipsTitle = resources.GetString("TipsTitle");
            Tip1 = resources.GetString("Tip1");
            Tip2= resources.GetString("Tip2");
            Tip3 = resources.GetString("Tip3");

            VoiceCommandsTitle = resources.GetString("VoiceCommandsTitle");
            VoiceCommandsIntro = resources.GetString("VoiceCommandsIntro");
            OpenCommandsSectionTitle = resources.GetString("OpenCommandsSectionTitle");
            OpenCommandsSectionIntro = resources.GetString("OpenCommandsSectionIntro");

            OpenCommandDescription1 = resources.GetString("OpenCommandDescription1");
            OpenCommandDescription2 = resources.GetString("OpenCommandDescription2");
            OpenCommandDescription3 = resources.GetString("OpenCommandDescription3");
            OpenCommandDescription4 = resources.GetString("OpenCommandDescription4");

            OpenCommand1 = resources.GetString("OpenCommand1");
            OpenCommand2 = resources.GetString("OpenCommand2");
            OpenCommand3 = resources.GetString("OpenCommand3");
            OpenCommand4 = resources.GetString("OpenCommand4");

            AboutTitle = resources.GetString("AboutTitle");
            AboutContent = resources.GetString("AboutContent");
            BroughtToYouByText = resources.GetString("BroughtToYouByText");
            ContactMeLinkText = resources.GetString("ContactMeLinkText");
            ContactMeContent = resources.GetString("ContactMeContent");
            DonateContent = resources.GetString("DonateContent");
            DonateButtonText = resources.GetString("DonateButtonText");
            ImageSourceText = resources.GetString("ImageSourceText");
            ImageSourceLinkText = resources.GetString("ImageSourceLinkText");
        }
    }
}
