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
        public ThoughtRecordDisplayModel ViewModel { get; private set; }
        private MainPage rootPage;

        public ThoughtRecordDisplayPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs obj)
        {
            base.OnNavigatedTo(obj);
            rootPage = ((App)Application.Current).CurrentMain;
            if (obj.Parameter != null)
            {
       
                rootPage.UpdateTitle(ThoughtRecordListModel.Title);
                int thoughtRecordId = Convert.ToInt32(obj.Parameter);
                ViewModel = new ThoughtRecordDisplayModel(AppDataService.GetDatabase(Application.Current));
                await ViewModel.InitializeModel(thoughtRecordId);
                if(ViewModel.ThoughtRecord == null)
                {
                    NavigateWithBackStackRemoval(typeof(ThoughtRecordListPage));
                }
                else
                {
                    ViewModel.OnThoughtRecordDeleteRequested += ConfirmDeleteRequest;
                    ViewModel.OnThoughtRecordDeleted += NavigatetoListPageAfterDelete;
                    ViewModel.OnThoughtRecordEditRequested += NavigateToEditPage;
                    ViewModel.OnThoughtRecordChanged += ScrollToTop;
                    rootPage.UpdateTitle(ThoughtRecordDisplayModel.Title);
                }

                rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordDisplayPage), thoughtRecordId);
            }
            else
            {
                NavigateWithBackStackRemoval(typeof(ThoughtRecordListPage));
            }
        }

        private void NavigateWithBackStackRemoval(Type pageType)
        {
            rootPage.NavigateWithMenuUpdate(pageType);
            if (Frame.BackStack.Count > 0)
            {
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
            }
        }
        private void ScrollToTop(object sender, EventArgs args)
        {
            ThoughtRecordScrollViewer.ChangeView(null, 0, null, false);
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

        public void NavigateToEditPage(object sender, EventArgs args)
        {
            rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordEditPage), ViewModel.ThoughtRecord.ThoughtRecordId);
        }

        private void NavigatetoListPageAfterDelete(object sender, EventArgs args)
        {
            rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordListPage));
            if(Frame.BackStack.Count > 0)
            {
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
            }
            RemoveThoughtRecordFromHistory();
        }

        /// <summary>
        /// Removes all pages from the Frame's BackStack where the deleted item's id is the parameter
        /// </summary>
        private void RemoveThoughtRecordFromHistory()
        {
            List<PageStackEntry> pageHistoryToDelete = Frame.BackStack.Where(b => (b.Parameter != null && (int)b.Parameter == ViewModel.ThoughtRecordId)).ToList();
            foreach (PageStackEntry page in pageHistoryToDelete)
            {
                Frame.BackStack.Remove(page);
            }
        }
    }
}
