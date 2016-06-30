using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.ViewModels;
using Windows.ApplicationModel.Activation;
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
using Windows.UI.Xaml.Media.Animation;
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
        public Page CurrentPage { get; private set; }
        public MainPage()
        {
            this.InitializeComponent();
            //Set this page as the current main page which other pages can access through the 
            //application object.
            ((App)(Application.Current)).CurrentMain = this;
            ViewModel = new MainViewModel();
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (MainFrame.CanGoBack)
                {
                    MainFrame.GoBack();
                    e.Handled = true;
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string command = e.Parameter as string;
            if (!string.IsNullOrEmpty(command))
            {
                ExecuteVoiceCommand(e.Parameter as string);
            }
            else
            {
                NewThoughtRecordMenuButton.IsChecked = true;
            }
        }

        private void ExecuteVoiceCommand(string voiceCommandName)
        {
            Type navigationPageType = null;
            object navigationParameter = null;

            switch (voiceCommandName)
            {
                case "OpenLatestRecord":
                    navigationPageType = typeof(ThoughtRecordDisplayPage);
                    navigationParameter = 0;
                    break;
                case "OpenRecordList":
                    navigationPageType = typeof(ThoughtRecordListPage);
                    break;
                case "OpenHelp":
                    navigationPageType = typeof(HelpPage);
                    break;
                case "OpenNewRecord":
                    navigationPageType = typeof(ThoughtRecordEditPage);
                    break;
                default:
                    navigationPageType = typeof(ThoughtRecordEditPage);
                    break;
            }

            if (navigationParameter == null)
            {
                NavigateWithMenuUpdate(navigationPageType);
            }
            else
            {
                NavigateWithMenuUpdate(navigationPageType, navigationParameter);
            }
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
            NewThoughtRecordMenuButton.IsChecked = false;
            ListThoughtRecordsMenuButton.IsChecked = false;
            HelpMenuButton.IsChecked = false;
            InformationMenuButton.IsChecked = false;
        }
        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        //Updates the main menu selection which will, in turn, trigger the navigation
        public void NavigateWithMenuUpdate(Type pageType, object navigationParameter = null)
        {
            if (pageType == typeof(ThoughtRecordEditPage))
            {
                    if (navigationParameter != null && (int)navigationParameter == 0)
                    {
                        NewThoughtRecordMenuButton.IsChecked = true;
                    }
                    else
                    {
                        MainFrame.Navigate(pageType, navigationParameter);
                        UpdateCurrentPage();
                    }
            }
            else if (pageType == typeof(ThoughtRecordListPage))
            {
                ListThoughtRecordsMenuButton.IsChecked = true;
            }
            else if (pageType == typeof(InformationPage))
            {
                InformationMenuButton.IsChecked = true;
            }
            else
            {
                ClearMenuSelection();
                if (pageType == typeof(ThoughtRecordDisplayPage) && MainFrame.CurrentSourcePageType != pageType)
                {
                    MainFrame.Navigate(pageType, navigationParameter);
                    UpdateCurrentPage();
                }
            }
        }

        public void UpdateTitle(string title)
        {
            ViewModel.Title = title;
        }

        private void UpdateCurrentPage()
        {
            CurrentPage = MainFrame.Content as Page;
        }

        private void MainMenuButton_Checked(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
            if ((bool)NewThoughtRecordMenuButton.IsChecked)
            {
                if (MainFrame.CurrentSourcePageType != typeof(ThoughtRecordEditPage) || (CurrentPage as ThoughtRecordEditPage).ViewModel.ThoughtRecord.ThoughtRecordId != 0)
                {
                    MainFrame.Navigate(typeof(ThoughtRecordEditPage), 0);
                    UpdateCurrentPage();
                }
            }
            else if ((bool)ListThoughtRecordsMenuButton.IsChecked)
            {
                if (MainFrame.CurrentSourcePageType != typeof(ThoughtRecordListPage))
                {
                    MainFrame.Navigate(typeof(ThoughtRecordListPage));
                    UpdateCurrentPage();
                }
            }
            else if ((bool)HelpMenuButton.IsChecked)
            {
                if (MainFrame.CurrentSourcePageType != typeof(HelpPage))
                {
                    MainFrame.Navigate(typeof(HelpPage));
                    UpdateCurrentPage();
                }
            }
            else if ((bool)InformationMenuButton.IsChecked)
            {
                if (MainFrame.CurrentSourcePageType != typeof(InformationPage))
                {
                    MainFrame.Navigate(typeof(InformationPage));
                    UpdateCurrentPage();
                }
            }
        }
    }
}
