﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordDAL.Models;
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
        public Emotion Emotion { get { return this.DataContext as Emotion; } }

        public InitialEmotionRatingTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        private void RemoveEmotionButton_Click(object sender, RoutedEventArgs e)
        {
            /*var x = this.Parent;
            Button clickedButton = (Button)sender;
            var emotion = (Emotion)clickedButton.DataContext;
            if (ViewModel.ThoughtRecord.Emotions.Count > 1)
            {
                ViewModel.ThoughtRecord.Emotions.Remove(emotion);
            }*/
        }

    }
}
