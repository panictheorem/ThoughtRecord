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
            rootPage.UpdateTitle(InformationModel.Title);
            rootPage.NavigateWithMenuUpdate(this.GetType());
            ViewModel = new InformationModel();
        }

        private async void DonationButton_Click(object sender, RoutedEventArgs e)
        {

            if (!(Application.Current as App).LicenseInformation.ProductLicenses["ThoughtRecordDonation"].IsActive)
            {
                try
                {
                    rootPage.ShowProgressRing();
                    //await CurrentApp.RequestProductPurchaseAsync("ThoughtRecordDonation", false);
                    var result = await CurrentAppSimulator.RequestProductPurchaseAsync("ThoughtRecordDonation", true);
                    //Check the license state to determine if the in-app purchase was successful.
                    
                }
                catch (Exception)
                {
                    // The in-app purchase was not completed because 
                    // an error occurred.
                }
                rootPage.HideProgressRing();
            }
            else
            {
                // The customer already owns this feature.
            }

        }

        private void ShowProgressRing(object sender, EventArgs args)
        {
            rootPage.ShowProgressRing();
        }

        private void HideProgressRing(object sender, EventArgs args)
        {
            rootPage.HideProgressRing();
        }
    }
}
