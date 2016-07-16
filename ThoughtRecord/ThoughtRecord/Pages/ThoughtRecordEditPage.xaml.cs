using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ThoughtRecordApp.ViewModels;
using ThoughtRecordApp.Templates;
using ThoughtRecordApp.DAL.Models;
using NotificationsExtensions.Toasts;
using Windows.UI.Notifications;
using ThoughtRecordApp.ViewModels.Infrastructure;
using System.Threading;
using Windows.UI.Popups;
using ThoughtRecordApp.Services;
using NotificationsExtensions;

namespace ThoughtRecordApp.Pages
{
    public sealed partial class ThoughtRecordEditPage : Page
    {
        public ThoughtRecordEditModel ViewModel { get; private set; }
        private MainPage rootPage;

        public ThoughtRecordEditPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs obj)
        {
            rootPage = (Application.Current as App).CurrentMain;
            int thoughtRecordId = Convert.ToInt32(obj.Parameter);
            if (thoughtRecordId != 0)
            {
                ViewModel = new ThoughtRecordEditModel(thoughtRecordId, AppDataService.GetDatabase(Application.Current));
            }
            else
            {
                ViewModel = new ThoughtRecordEditModel(AppDataService.GetDatabase(Application.Current));

            }
            rootPage.UpdateTitle(ViewModel.Title);
            ViewModel.OnThoughtRecordSaving += ShowProgressRing;
            ViewModel.OnThoughtRecordSaved += HideProgressRing;
            ViewModel.OnNewThoughtRecordOverwriteRisk += PromptToSave;
            ViewModel.OnNewThoughtRecordCreated += UpdateMainMenu;
            this.Unloaded += ThoughtRecordEditPage_Unloaded;
            rootPage.NavigateWithMenuUpdate(this.GetType(), thoughtRecordId);
            base.OnNavigatedTo(obj);
        }

        private void UpdateMainMenu(object sender, EventArgs args)
        {
            rootPage.UpdateTitle(ViewModel.Title);
            rootPage.NavigateWithMenuUpdate(typeof(ThoughtRecordEditPage), 0);
            ThoughtRecordScrollViewer.ChangeView(null, 0, null, false);
        }

        private async void PromptToSave(object sender, EventArgs args)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Your thought record is not saved",
                Content = "Would you like to save this thought record?",
                PrimaryButtonText = "Save",
                PrimaryButtonCommand = new RelayCommand(() =>
                {
                    ViewModel.Save.Execute(null);
                    ViewModel.CreateNewThoughtRecord();
                }),
                SecondaryButtonText = "Don't Save",
                SecondaryButtonCommand = new RelayCommand(() =>
                {
                    ViewModel.CreateNewThoughtRecord();
                }),
            };
            await dialog.ShowAsync();
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (!ViewModel.IsCurrentDataSaved)
            {
                e.Cancel = true;
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Your Thought Record Is Not Saved",
                    Content = "Would you like to save this thought record?",
                    PrimaryButtonText = "Save",
                    PrimaryButtonCommand = ViewModel.Save,
                    SecondaryButtonText = "Don't Save"
                };
                await dialog.ShowAsync();
                ViewModel.IsCurrentDataSaved = true;
                Frame.Navigate(e.SourcePageType, e.Parameter);

            }
        }

        private void ShowProgressRing(object sender, EventArgs args)
        {
            rootPage.ShowProgressRing();
        }

        private void HideProgressRing(object sender, EventArgs args)
        {
            rootPage.HideProgressRing();
            DisplaySaveConfirmationMessage();
        }

        private void DisplaySaveConfirmationMessage()
        {
            rootPage.DisplaySaveConfirmationAlert();

        }

        private void AddEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Emotions.Add(new Emotion() { InitialRating = 50, SubsequentRating = 50 });
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && ViewModel.DefaultInputText != null)
            {
                if (ViewModel.DefaultInputText.Contains(textBox.Text))
                {
                    textBox.Text = string.Empty;
                }
            }
        }

        private void InitialEmotionRatingTemplate_RemoveButtonClicked(object sender, RemoveEmotionButtonClickedEventArgs args)
        {
            ViewModel.Emotions.Remove(args.SelectedEmotion);
        }

        /// <summary>
        /// Clear a textbox if it contains the default text.
        /// </summary>
        private void InitialEmotionRatingTemplate_TextBoxGotFocus(object sender, EmotionAutoSuggestBoxHasFocusEventArgs args)
        {
            var textBox = args.EmotionAutoSuggestBox;
            if (textBox != null && ViewModel.DefaultInputText != null)
            {
                if (ViewModel.DefaultInputText.Contains(textBox.Text))
                {
                    textBox.Text = "";
                }
            }
        }

        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (ViewModel.IsCurrentDataSaved)
            {
                ViewModel.IsCurrentDataSaved = false;
            }
        }

        private void ThoughtRecordEditPage_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Dispose();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                if (textBox == SituationDescriptionTextBox)
                {
                    textBox.Text = ViewModel.DefaultInputText.ElementAt(0);
                }
                else if (textBox == AutomaticThoughtsTextBox)
                {
                    textBox.Text = ViewModel.DefaultInputText.ElementAt(1);
                }
                else if (textBox == SupportingEvidenceTextBox)
                {
                    textBox.Text = ViewModel.DefaultInputText.ElementAt(2);
                }
                else if (textBox == ContradictingEvidenceTextBox)
                {
                    textBox.Text = ViewModel.DefaultInputText.ElementAt(4);
                }
                else if (textBox == RationalAssessmentTextBox)
                {
                    textBox.Text = ViewModel.DefaultInputText.ElementAt(5);
                }
            }
        }
    }
}
