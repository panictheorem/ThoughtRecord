using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ThoughtRecordApp.DAL.Models;
using ThoughtRecordApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ThoughtRecordApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThoughtRecordListPage : Page
    {
        public ThoughtRecordListModel ViewModel = new ThoughtRecordListModel();

        public ThoughtRecordListPage()
        {
            this.InitializeComponent();
        }

        private void ThoughtRecordGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ThoughtRecord selectedThoughtRecord = e.ClickedItem as ThoughtRecord;
            Frame.Navigate(typeof(ThoughtRecordEditPage), selectedThoughtRecord.ThoughtRecordId);
        }
        
    }
}
