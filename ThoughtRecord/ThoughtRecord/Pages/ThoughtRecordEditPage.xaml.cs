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

namespace ThoughtRecordApp.Pages
{
    public sealed partial class ThoughtRecordEditPage : Page
    {
        private ThoughtRecordEditModel ViewModel;
        private MainPage rootPage;
        public ThoughtRecordEditPage()
        {
            this.InitializeComponent();
            rootPage = (Application.Current as App).CurrentMain;
        }
        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (!ViewModel.IsCurrentDataSaved)
            {
                e.Cancel = true;
                MessageDialog dialog = new MessageDialog("Would you like to save this thought record?", "Your Thought Record Is Not Saved");
                dialog.Commands.Add(new UICommand("Save") { Id = 0 });
                dialog.Commands.Add(new UICommand("Don't Save") { Id = 2 });
                var result = await dialog.ShowAsync();
                if (Convert.ToInt16(result.Id) == 0)
                {
                    ViewModel.Save.Execute(null);
                }
                ViewModel.IsCurrentDataSaved = true;
                Frame.Navigate(e.SourcePageType);
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
            string title = "Saved";
            string content = "Your thought record has been saved.";

            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText()
                {
                    Text = title
                },

                BodyTextLine1 = new ToastText()
                {
                    Text = content
                }
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual
            };

            var toast = new ToastNotification(toastContent.GetXml());

            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

        protected override void OnNavigatedTo(NavigationEventArgs obj)
        {
            int thoughtRecordId = Convert.ToInt32(obj.Parameter);
            if (thoughtRecordId != 0)
            {
                ViewModel = new ThoughtRecordEditModel(thoughtRecordId, AppDataService.GetDatabase(Application.Current));
            }
            else
            {
                ViewModel = new ThoughtRecordEditModel(AppDataService.GetDatabase(Application.Current));
            }
            ViewModel.OnThoughtRecordSaving += ShowProgressRing;
            ViewModel.OnThoughtRecordSaved += HideProgressRing;
            base.OnNavigatedTo(obj);
        }

        private void AddEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Emotions.Add(new Emotion() { Name = "" });
            ViewModel.IsCurrentDataSaved = false;
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
            ViewModel.IsCurrentDataSaved = false;
        }

        private void InitialEmotionRatingTemplate_TextBoxGotFocus(object sender, EmotionTextBoxHasFocusEventArgs args)
        {
            var textBox = args.EmotionTextBox;
            if (textBox != null && ViewModel.DefaultInputText != null)
            {
                if (ViewModel.DefaultInputText.Contains(textBox.Text))
                {
                    textBox.Text = "";
                }
            }
        }
    }
}
