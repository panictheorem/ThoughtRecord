using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.Services;
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

namespace ThoughtRecordApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThoughtRecordDisplayPage : Page
    {
        private ThoughtRecordDisplayModel ViewModel;
        private MainPage rootPage;

        public ThoughtRecordDisplayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs obj)
        {
            rootPage = ((App)Application.Current).CurrentMain;
            if (obj.Parameter != null)
            {
                int thoughtRecordId = Convert.ToInt32(obj.Parameter);
                ViewModel = new ThoughtRecordDisplayModel(thoughtRecordId, AppDataService.GetDatabase(Application.Current));
                rootPage.UpdateTitle(ThoughtRecordListModel.Title);
                ViewModel.OnThoughtRecordDeleteRequested += ConfirmDeleteRequest;
                ViewModel.OnThoughtRecordDeleted += NavigatetoListPageAfterDelete;
                ViewModel.OnThoughtRecordEditRequested += NavigateToEditPage;
                rootPage.NavigateWithMenuUpdate(this.GetType());
                base.OnNavigatedTo(obj);
            }
            else
            {
                rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordListPage));
                if(Frame.BackStack.Count > 0)
                {
                    Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
                }
            }
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
            rootPage.UpdateTitle("Edit Thought Record");
        }

        private void NavigatetoListPageAfterDelete(object sender, EventArgs args)
        {
            rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordListPage));
            if(Frame.BackStack.Count > 0)
            {
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
            }
        }
    }
}
