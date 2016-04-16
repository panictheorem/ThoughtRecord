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

        private void InitialEmotionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var emotionListView = ((ListView)sender);
            var index = emotionListView.SelectedIndex;
            if(index != -1)
            {
                ViewModel.ThoughtRecord.Emotions.RemoveAt(index);
            }
        }
    }
}
