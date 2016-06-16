using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.Services;
using ThoughtRecordApp.ViewModels;
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
    /// Displays a list of saved thought records.
    /// </summary>
    public sealed partial class ThoughtRecordListPage : Page
    {
        private MainPage rootPage;
        public ThoughtRecordListModel ViewModel;

        public ThoughtRecordListPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = ((App)(Application.Current)).CurrentMain;
            ViewModel = new ThoughtRecordListModel(AppDataService.GetDatabase(Application.Current));
            rootPage.UpdateTitle(ThoughtRecordListModel.Title);
            rootPage.NavigateWithMenuUpdate(this.GetType());
            await InitializeViewModel();
            base.OnNavigatedTo(e);
        }
        private async Task InitializeViewModel()
        {
            rootPage.ShowProgressRing();
            await ViewModel.Initialize();
            if (ViewModel == null || ViewModel.ThoughtRecords == null || ViewModel.ThoughtRecords.Count == 0)
            {
                NoThoughtRecordMessage.Visibility = Visibility.Visible;
            }
            else
            {
                NoThoughtRecordMessage.Visibility = Visibility.Collapsed;
            }
            rootPage.HideProgressRing();
        }

        //Navigates to the display page for the selected item.
        private void ThoughtRecordItemsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Situation selectedSituationRecord = e.ClickedItem as Situation;
            rootPage.ClearMenuSelection();
            Frame.Navigate(typeof(ThoughtRecordDisplayPage), selectedSituationRecord.ThoughtRecordId);
        }

        private void NewThoughtRecordButton_Click(object sender, RoutedEventArgs e)
        {
            rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordEditPage));
        }
    }
}
