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
            PageTitle.Text = "New Thought Record";
            NewThoughtRecordListBoxItem.IsSelected = true;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (MainFrame.CanGoBack)
                {
                    MainFrame.GoBack();
                    NavigateWithMenuUpdate(MainFrame.CurrentSourcePageType);
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

        //Updates header title and navigates, if necessary, based on selected item
        private void MainMenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
            if (NewThoughtRecordListBoxItem.IsSelected)
            {
                //We don't check if already on the page here because the user may be on the edit page
                //for a record, but then press the new thought record button. Since they use the same page,
                //we want to be able to navigate from editing a thought record to a new record.
                MainFrame.Navigate(typeof(ThoughtRecordEditPage));
                PageTitle.Text = "New Thought Record";
            }
            else if (ListThoughtRecordsListBoxItem.IsSelected)
            {
                if (MainFrame.CurrentSourcePageType != typeof(ThoughtRecordListPage))
                {
                    MainFrame.Navigate(typeof(ThoughtRecordListPage));
                }
                PageTitle.Text = "My Thought Records";
            }
            else if (InformationListBoxItem.IsSelected)
            {
                if (MainFrame.CurrentSourcePageType != typeof(InformationPage))
                {
                    MainFrame.Navigate(typeof(InformationPage));
                }
                PageTitle.Text = "Information";
            }
        }
    }
}
