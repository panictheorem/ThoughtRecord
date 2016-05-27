using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.Pages;
using ThoughtRecordApp.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ThoughtRecordApp.Templates
{
    public sealed partial class InitialEmotionRatingTemplate : UserControl
    {
        private List<string> emotionNameSuggestions;
        private ObservableCollection<Emotion> Emotions;
        public Emotion Emotion { get { return this.DataContext as Emotion; } }
        public delegate void RemoveEmotionButtonClickedEvent(object sender, RemoveEmotionButtonClickedEventArgs args);
        public delegate void TextBoxGotFocusEvent(object sender, EmotionAutoSuggestBoxHasFocusEventArgs args);
        public event RemoveEmotionButtonClickedEvent RemoveButtonClicked;
        public event TextBoxGotFocusEvent TextBoxGotFocus;

        public InitialEmotionRatingTemplate()
        {
            this.InitializeComponent();
            emotionNameSuggestions = EmotionService.GetEmotionNameSuggestions();
            this.DataContextChanged += (s, e) => Bindings.Update();
            this.Loaded += InitialEmotionRatingTemplate_Loaded;
        }

        private void InitialEmotionRatingTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            var parentPage = FindParent<ThoughtRecordEditPage>(this);
            Emotions = parentPage.ViewModel.Emotions;
            Emotions.CollectionChanged += Emotions_CollectionChanged;
            ToggleRemoveButton();
        }

        private void Emotions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ToggleRemoveButton();
        }

        private void ToggleRemoveButton()
        {
            if (Emotions.Count > 1)
            {
                if (RemoveEmotionButton.Visibility != Visibility.Visible)
                {
                    ShowRemoveButton();
                }
            }
            else
            {
                HideRemoveButton();
            }
        }

        private void RemoveEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            var emotion = (Emotion)clickedButton.DataContext;
            RemoveButtonClicked?.Invoke(this, new RemoveEmotionButtonClickedEventArgs(emotion));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus?.Invoke(this, new EmotionAutoSuggestBoxHasFocusEventArgs(sender as AutoSuggestBox));
        }

        private void EmotionNameAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            UpdateEmotionSuggestions();
        }

        private void EmotionNameAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent())
            {
                UpdateEmotionSuggestions();
            }
        }

        private void EmotionNameAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            EmotionNameAutoSuggestBox.Text = args.SelectedItem as string;
        }

        private void UpdateEmotionSuggestions()
        {
            var query = EmotionNameAutoSuggestBox.Text.ToLower();
            var matchingEmotions = emotionNameSuggestions.Where(e => e.ToLower().Contains(query)).ToList();
            EmotionNameAutoSuggestBox.ItemsSource = matchingEmotions;
        }

        private void ShowRemoveButton()
        {
            RemoveEmotionButton.Visibility = Visibility.Visible;
        }

        private void HideRemoveButton()
        {
            RemoveEmotionButton.Visibility = Visibility.Collapsed;
        }

        private static T FindParent<T>(DependencyObject element) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(element);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void DecrementEmotionRating()
        {
            if (Emotion.InitialRating > 0)
            {
                Emotion.InitialRating--;
            }
        }
        private void IncrementEmotionRating()
        {
            if (Emotion.InitialRating < 100)
            {
                Emotion.InitialRating++;
            }
        }

        private void DecrementEmotionRatingButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementEmotionRating();
            if (Emotion.InitialRating == 0)
            {
                DecrementEmotionRatingButton.IsEnabled = false;
            }
            if (Emotion.InitialRating == 99)
            {
                IncrementEmotionRatingButton.IsEnabled = true;
            }
        }

        private void IncrementEmotionRatingButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementEmotionRating();
            if(Emotion.InitialRating == 100)
            {
                IncrementEmotionRatingButton.IsEnabled = false;
            }
            if (Emotion.InitialRating == 1)
            {
                DecrementEmotionRatingButton.IsEnabled = true;
            }
        }
    }
    public class EmotionAutoSuggestBoxHasFocusEventArgs : RoutedEventArgs
    {
        public AutoSuggestBox EmotionAutoSuggestBox { get; }
        public EmotionAutoSuggestBoxHasFocusEventArgs(AutoSuggestBox autoSuggestBox)
        {
            EmotionAutoSuggestBox = autoSuggestBox;
        }
    }
    public class RemoveEmotionButtonClickedEventArgs : RoutedEventArgs
    {
        public Emotion SelectedEmotion { get; }

        public RemoveEmotionButtonClickedEventArgs(Emotion selectedEmotion)
        {
            SelectedEmotion = selectedEmotion;
        }
    }
}
