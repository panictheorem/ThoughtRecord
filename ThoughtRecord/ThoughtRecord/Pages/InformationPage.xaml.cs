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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ThoughtRecordApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InformationPage : Page
    {
        private MainPage rootPage;
        public InformationPage()
        {
            this.InitializeComponent();
            rootPage = (Application.Current as App).CurrentMain;
            rootPage.UpdateTitle(InformationModel.Title);
            rootPage.NavigateWithMenuUpdate(this.GetType());
        }

        private async void DonationButton_Click(object sender, RoutedEventArgs e)
        {

            if (!(Application.Current as App).LicenseInformation.ProductLicenses["ThoughtRecordDonation"].IsActive)
            {
                try
                {
                    //await CurrentApp.RequestProductPurchaseAsync("ThoughtRecordDonation", false);
                    await CurrentAppSimulator.RequestProductPurchaseAsync("ThoughtRecordDonation", false);
                    //Check the license state to determine if the in-app purchase was successful.
                }
                catch (Exception)
                {
                    // The in-app purchase was not completed because 
                    // an error occurred.
                }
            }
            else
            {
                // The customer already owns this feature.
            }

        }
    }
}
