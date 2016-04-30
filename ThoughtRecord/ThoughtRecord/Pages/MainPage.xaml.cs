using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ThoughtRecordApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
            MainFrame.Navigate(typeof(ThoughtRecordEditPage));
            PageTitle.Text = "New Thought Record";
            NewThoughtRecordListBoxItem.IsSelected = true;
            var editPage = this.MainFrame.Content as ThoughtRecordEditPage;
            editPage.OnAsyncOperationStart += ShowProgressRing;
            editPage.OnAsyncOperationEnd += HideProgressRing;
        }

        private void ShowProgressRing(object sender, EventArgs args)
        {
            MainProgressRing.IsActive = true;
        }

        private void HideProgressRing(object sender, EventArgs args)
        {
            MainProgressRing.IsActive = false;
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void MainMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewThoughtRecordListBoxItem.IsSelected)
            {
                MainFrame.Navigate(typeof(ThoughtRecordEditPage));
                PageTitle.Text = "New Thought Record";
            }
            else if (ListThoughtRecordsListBoxItem.IsSelected)
            {
                MainFrame.Navigate(typeof(ThoughtRecordListPage));
                PageTitle.Text = "My Thought Records";
            }
            else if (SettingsListBoxItem.IsSelected)
            {
                MainFrame.Navigate(typeof(SettingsPage));
                PageTitle.Text = "Settings";
            }
            else if (InformationListBoxItem.IsSelected)
            {
                MainFrame.Navigate(typeof(InformationPage));
                PageTitle.Text = "Information";
            }

        }
    }
}
