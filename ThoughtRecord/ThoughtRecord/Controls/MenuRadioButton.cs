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
    /// <summary>
    /// A custom radio button class to give more control over visual states during button's lifecyle
    /// </summary>
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
        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            this.IsChecked = true;
            if (!(bool)IsChecked)
            {
                VisualStateManager.GoToState(this, "Normal", false);
            }
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            if (!(bool)IsChecked)
            {
                VisualStateManager.GoToState(this, "Pressed", false);
            }
        }
    }
}
