using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public Emotion Emotion { get { return this.DataContext as Emotion; } }
        public delegate void RemoveEmotionButtonClickedEvent(object sender, RemoveEmotionButtonClickedEventArgs args);
        public delegate void TextBoxGotFocusEvent(object sender, EmotionTextBoxHasFocusEventArgs args);
        public event RemoveEmotionButtonClickedEvent RemoveButtonClicked;
        public event TextBoxGotFocusEvent TextBoxGotFocus;


        public InitialEmotionRatingTemplate()
        {
            this.InitializeComponent();
            emotionNameSuggestions = EmotionService.GetEmotionNameSuggestions();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        private void RemoveEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            var emotion = (Emotion)clickedButton.DataContext;
            RemoveButtonClicked?.Invoke(this, new RemoveEmotionButtonClickedEventArgs(emotion));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus?.Invoke(this, new EmotionTextBoxHasFocusEventArgs(sender as TextBox));
        }

        private void EmotionNameAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void EmotionNameAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.CheckCurrent())
            {
                var query = EmotionNameAutoSuggestBox.Text.ToLower();
            }
        }

        private void EmotionNameAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            EmotionNameAutoSuggestBox.Text = args.SelectedItem as string;
        }
    }

    public class EmotionTextBoxHasFocusEventArgs : RoutedEventArgs
    {
        public TextBox EmotionTextBox { get; }
        public EmotionTextBoxHasFocusEventArgs(TextBox textBox)
        {
            EmotionTextBox = textBox;
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
