using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ThoughtRecordApp.Controls
{
    public class MenuRadioButton : RadioButton
    {
        protected override void OnPointerCaptureLost(PointerRoutedEventArgs e)
        {
            if (!(bool)IsChecked)
            {
                VisualStateManager.GoToState(this, "Normal", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Checked", false);
            }
        }
        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            if (!(bool)IsChecked)
            {
                VisualStateManager.GoToState(this, "PointerOver", false);
            }
        }
    }
}
