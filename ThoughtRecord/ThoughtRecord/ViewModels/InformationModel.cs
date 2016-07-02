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

        public string AboutTitle { get; private set; }
        public string BroughtToYouByText { get; private set; }

        public string FeedbackTitle { get; private set; }
        public string ContactMeContent { get; private set; }
        public string FeedbackButtonText { get; private set; }
        public string ContactMeLinkText { get; private set; }

        public string SupportAppTitle { get; private set; }
        public string RatingContent { get; set; }
        public string RatingButtonText { get; set; }
        public string DonateContent { get; private set; }
        public string DonateButtonText { get; set; }

        public string CreditsTitle { get; set; }
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

            AboutTitle = stringLoader.GetString("AboutTitle");
            BroughtToYouByText = stringLoader.GetString("BroughtToYouByText");

            FeedbackTitle = stringLoader.GetString("FeedbackTitle");
            ContactMeContent = stringLoader.GetString("ContactMeContent");
            FeedbackButtonText = stringLoader.GetString("FeedbackButtonText");
            ContactMeLinkText = stringLoader.GetString("ContactMeLinkText");


            SupportAppTitle = stringLoader.GetString("SupportAppTitle");
            RatingContent = stringLoader.GetString("RatingContent");
            RatingButtonText = stringLoader.GetString("RatingButtonText");
            DonateContent = stringLoader.GetString("DonateContent");
            DonateButtonText = stringLoader.GetString("DonateButtonText");

            CreditsTitle = stringLoader.GetString("CreditsTitle");
            ImageSourceText = stringLoader.GetString("ImageSourceText");
            ImageSourceLinkText = stringLoader.GetString("ImageSourceLinkText");
        }
    }
}
