using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.ViewModels.Infrastructure;

namespace ThoughtRecordApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private int selectedIndex;

        public int SelectedIndex {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
    }
}
