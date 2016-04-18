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
using ThoughtRecordDAL.Models;
using ThoughtRecordApp.Templates;

namespace ThoughtRecordApp.Pages
{
    public sealed partial class ThoughtRecordDisplay : Page
    {
        private ThoughtRecordDisplayModel ViewModel;

        public ThoughtRecordDisplay()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs obj)
        {
            int thoughtRecordId = Convert.ToInt32(obj.Parameter);
            if (thoughtRecordId != 0)
            {
                ViewModel = new ThoughtRecordDisplayModel(thoughtRecordId);
            }
            else
            {
                ViewModel = new ThoughtRecordDisplayModel();
            }
            base.OnNavigatedTo(obj);
        }

        private void AddEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ThoughtRecord.Emotions.Add(new ThoughtRecordDAL.Models.Emotion());
        }
        
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if(ViewModel.DefaultInputText.Contains(textBox.Text))
                {
                    textBox.Text = string.Empty;
                }
            }
        }

        private void InitialEmotionRatingTemplate_RemoveButtonClicked(object sender, RemoveEmotionButtonClickedEventArgs args)
        {
            if (ViewModel.ThoughtRecord.Emotions.Count > 1)
            {
                ViewModel.ThoughtRecord.Emotions.Remove(args.SelectedEmotion);
            }
        }
    }
}
