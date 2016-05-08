using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThoughtRecordApp.ViewModels.Infrastructure
{
    /// <summary>
    /// Implementation of ICommand to be used by View Models to define commands
    /// which can be bound to UI elements.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action executeMethod;
        private Func<bool> canExecuteMethod;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public RelayCommand(Action executeMethod) : this(executeMethod, null) { }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteMethod == null)
            {
                return true;
            }
            else
            {
                return canExecuteMethod.Invoke();
            }
        }

        public void Execute(object parameter)
        {
            executeMethod?.Invoke();
        }
    }
}
