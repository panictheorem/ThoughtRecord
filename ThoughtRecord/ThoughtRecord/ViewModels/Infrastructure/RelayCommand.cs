using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThoughtRecordApp.ViewModels.Infrastructure
{
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
