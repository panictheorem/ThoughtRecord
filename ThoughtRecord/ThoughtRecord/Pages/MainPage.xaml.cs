using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
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
    /// The root page of the application. This page hosts the frame with which 
    /// all other pages are displayed. It also contains the header title and 
    /// main menu navigation UI. 
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            //Set this page as the current main page which other pages can access through the 
            //application object.
            ((App)(Application.Current)).CurrentMain = this;
            ViewModel = new MainViewModel();
            NewThoughtRecordListBoxItem.IsSelected = true;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (MainFrame.CanGoBack)
                {
                    MainFrame.GoBack();
                    e.Handled = true;
                }
            };
        }

        public void ShowProgressRing()
        {
            MainProgressRing.IsActive = true;
        }

        public void HideProgressRing()
        {
            MainProgressRing.IsActive = false;
        }
        public void ClearMenuSelection()
        {
            MainMenuListBox.SelectedItem = null;
        }
        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }
        //Updates the main menu selection which will, in turn, trigger the navigation
        public void NavigateWithMenuUpdate(Type type)
        {
            if (type == typeof(ThoughtRecordEditPage))
            {
                NewThoughtRecordListBoxItem.IsSelected = true;
            }
            else if (type == typeof(ThoughtRecordListPage))
            {
                ListThoughtRecordsListBoxItem.IsSelected = true;
            }
            else if (type == typeof(InformationPage))
            {
                InformationListBoxItem.IsSelected = true;
            }
            else
            {
                MainMenuListBox.SelectedItem = null;
            }
        }
        public void UpdateTitle(string title)
        {
            ViewModel.Title = title;
        }
        //Updates navigates to the page based on the selection.
        //You can't navigate to a page you are already on, with the exception of the Edit Page
        private void MainMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
            if (NewThoughtRecordListBoxItem.IsSelected)
            {
                ThoughtRecordEditPage currentEditPage = MainFrame.Content as ThoughtRecordEditPage;
               
                if (MainFrame.CurrentSourcePageType != typeof(ThoughtRecordEditPage) ||
                    //We want to allow the user to be able to navigate to the New Thought Record page
                    //if they are currently on the edit page. Since they use the same page, we also allow 
                    //navigation if the current thought record's id is not 0
                    (currentEditPage != null && currentEditPage.ViewModel.ThoughtRecord.ThoughtRecordId != 0))
                {
                    MainFrame.Navigate(typeof(ThoughtRecordEditPage), 0);
                }
            }
            else if (ListThoughtRecordsListBoxItem.IsSelected)
            {
                if (MainFrame.CurrentSourcePageType != typeof(ThoughtRecordListPage))
                {
                    MainFrame.Navigate(typeof(ThoughtRecordListPage));
                }
            }
            else if (InformationListBoxItem.IsSelected)
            {

                if (MainFrame.CurrentSourcePageType != typeof(InformationPage))
                {
                    MainFrame.Navigate(typeof(InformationPage));
                }
            }
        }
    }
}
