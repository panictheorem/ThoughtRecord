using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class ThoughtRecordDisplayPage : Page
    {
        private ThoughtRecordDisplayModel ViewModel;

        public ThoughtRecordDisplayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs obj)
        {
            int thoughtRecordId = Convert.ToInt32(obj.Parameter);
            ViewModel = new ThoughtRecordDisplayModel(thoughtRecordId);
            ViewModel.OnThoughtRecordDeleteRequested += ConfirmDeleteRequest;
            ViewModel.OnThoughtRecordDeleted += NavigatetoListPage;
            ViewModel.OnThoughtRecordEditRequested += NavigateToEditPage;
            base.OnNavigatedTo(obj);
        }

        private async void ConfirmDeleteRequest(object sender, EventArgs args)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Delete Thought Record",
                Content = "Are you sure you would like to delete this thought record?",
                PrimaryButtonText = "Ok",
                PrimaryButtonCommand = ViewModel.Delete,
                SecondaryButtonText = "Cancel"
            };
            await dialog.ShowAsync();

        }

        private void NavigateToEditPage(object sender, EventArgs args)
        {
            Frame.Navigate(typeof(ThoughtRecordEditPage), ViewModel.ThoughtRecord.ThoughtRecordId);
        }

        private void NavigatetoListPage(object sender, EventArgs args)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                Frame.Navigate(typeof(ThoughtRecordListPage));
            }
        }
    }
}
