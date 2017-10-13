using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserManager
{
    public class OneParameterCommand<T> : ICommand where T : class
    {
        private readonly Action<T> executeAction;

        public event EventHandler CanExecuteChanged;

        public OneParameterCommand(Action<T> ExecuteAction)
        {
            executeAction = ExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction.Invoke(parameter as T);
        }
    }
}
