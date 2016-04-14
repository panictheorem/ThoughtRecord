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
using System.ComponentModel;

namespace ThoughtRecordApp.Pages
{
    public sealed partial class ThoughtRecordDisplay : Page, INotifyPropertyChanged
    {
        private ThoughtRecordDisplayModel ViewModel = new ThoughtRecordDisplayModel();
        public ThoughtRecordDisplay()
        {
            this.InitializeComponent();
            DateOfSituationPicker.Date = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ThoughtRecord.Emotions.Add(new ThoughtRecordDAL.Models.Emotion() { Name = "Stressed"});
        }

        private void EmotionNameTextBox_TextChanging(object sender, TextBoxTextChangingEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewModel.Emotions.Name"));
        }
    }
}
