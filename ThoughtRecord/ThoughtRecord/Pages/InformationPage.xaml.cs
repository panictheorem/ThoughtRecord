using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.ViewModels;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace ThoughtRecordApp.Pages
{
    /// <summary>
    /// Page containing general information about the app.
    /// </summary>
    public sealed partial class InformationPage : Page
    {
        public InformationModel ViewModel { get; private set; }
        private MainPage rootPage;
        public InformationPage()
        {
            this.InitializeComponent();
            rootPage = (Application.Current as App).CurrentMain;
            ViewModel = new InformationModel();
            rootPage.UpdateTitle(InformationModel.Title);
            rootPage.NavigateWithMenuUpdate(this.GetType());
            if (Microsoft.Services.Store.Engagement.Feedback.IsSupported)
            {
                this.FeedbackHubArea.Visibility = Visibility.Visible;
            }
        }

        private async void DonationButton_Click(object sender, RoutedEventArgs e)
        {
            DonationButton.Visibility = Visibility.Collapsed;
            try
            {
                rootPage.ShowProgressRing();
#if DEBUG
                var result = await CurrentAppSimulator.RequestProductPurchaseAsync("ThoughtRecordDonation", true);
#else
                var result = await CurrentApp.RequestProductPurchaseAsync("ThoughtRecordDonation", true);
#endif
                //Check the license state to determine if the in-app purchase was successful.

                if ((Application.Current as App).LicenseInformation.ProductLicenses["ThoughtRecordDonation"].IsActive)
                {
                    DonationSuccessMessage.Text = "Thanks! :)";
                }
                else
                {
                    DonationSuccessMessage.Text = "The Donation could not be processed. Try again later.";
                }
            }
            catch (Exception)
            {
                // The in-app purchase was not completed because an error occurred.
                DonationSuccessMessage.Text = "An error occurred and the donation could not be processed. Try again later.";

            }
            rootPage.HideProgressRing();
    }

    private void ShowProgressRing(object sender, EventArgs args)
    {
        rootPage.ShowProgressRing();
    }

    private void HideProgressRing(object sender, EventArgs args)
    {
        rootPage.HideProgressRing();
    }

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            await Microsoft.Services.Store.Engagement.Feedback.LaunchFeedbackAsync();
        }
    }
}
