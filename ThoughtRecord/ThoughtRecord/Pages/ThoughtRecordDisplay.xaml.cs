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

namespace ThoughtRecordApp.Pages
{
    public sealed partial class ThoughtRecordDisplay : Page
    {
        private ThoughtRecordDisplayModel ViewModel = new ThoughtRecordDisplayModel();

        public ThoughtRecordDisplay()
        {
            this.InitializeComponent();
            ViewModel.ThoughtRecord.Situation.DateTime = DateTime.Now;
        }

        private void AddEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ThoughtRecord.Emotions.Add(new ThoughtRecordDAL.Models.Emotion() { Name = "Stressed" });
        }

        private void EmotionNameAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            AutoSuggestBox emotionSuggestBox = sender as AutoSuggestBox;
            emotionSuggestBox.ItemsSource = ViewModel.EmotionNameSuggestions.Where(e => emotionSuggestBox.Text.ToLower().Contains(e.ToLower()));
            emotionSuggestBox.Focus(FocusState.Pointer);
        }

        private void EmotionNameAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            AutoSuggestBox emotionSuggestBox = sender as AutoSuggestBox;
            emotionSuggestBox.ItemsSource = ViewModel.EmotionNameSuggestions.Where(e => args.QueryText.ToLower().Contains(e.ToLower()));
            emotionSuggestBox.Focus(FocusState.Pointer);
        }

        private void EmotionNameAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            ((AutoSuggestBox)sender).Text = args.SelectedItem as string;
        }
    }
}
