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

namespace ThoughtRecordApp.Pages
{
    public sealed partial class ThoughtRecordEditPage : Page
    {
        private ThoughtRecordEditModel ViewModel;
        private MainPage mainPage;

        public ThoughtRecordEditPage()
        {
            this.InitializeComponent();
            mainPage = ((App)Application.Current).CurrentMain; 
        }

        private void ShowProgressRing(object sender, EventArgs args)
        {
            mainPage.ShowProgressRing();
        }

        private void HideProgressRing(object sender, EventArgs args)
        {
            mainPage.HideProgressRing();
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
                ViewModel = new ThoughtRecordEditModel(thoughtRecordId);
            }
            else
            {
                ViewModel = new ThoughtRecordEditModel();
            }
            ViewModel.OnThoughtRecordSaving += ShowProgressRing;
            ViewModel.OnThoughtRecordSaved += HideProgressRing;
            base.OnNavigatedTo(obj);
        }

        private void AddEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Emotions.Add(new Emotion());
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
