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
        public const string Title = "Information";

        public string InstructionsTitle { get; private set; }
        public string TipsTitle { get; private set; }
        public string AboutTitle { get; private set; }
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
        public string Tip1 { get; private set; }
        public string Tip2 { get; private set; }
        public string Tip3 { get; private set; }
        public string AboutContent { get; private set; }
        public string BroughtToYouByText { get; private set; }
        public string ContactMeLinkText { get; private set; }
        public string ContactMeContent { get; private set; }
        public string DonateContent { get; private set; }
        public string DonateButtonText { get; set; }
        public InformationModel()
        {
            InitializeContent();
        }

        private void InitializeContent()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("InformationText");

            InstructionsTitle = resources.GetString("InstructionsTitle");
            TipsTitle = resources.GetString("TipsTitle");
            AboutTitle = resources.GetString("AboutTitle");
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
            Tip1 = resources.GetString("Tip1");
            Tip2= resources.GetString("Tip2");
            Tip3 = resources.GetString("Tip3");
            AboutContent = resources.GetString("AboutContent");
            BroughtToYouByText = resources.GetString("BroughtToYouByText");
            ContactMeLinkText = resources.GetString("ContactMeLinkText");
            ContactMeContent = resources.GetString("ContactMeContent");
            DonateContent = resources.GetString("DonateContent");
            DonateButtonText = resources.GetString("DonateButtonText");
        }
    }
}
