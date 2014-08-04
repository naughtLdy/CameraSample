using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CameraSample.Delegates
{
	class DelegateCommand : ICommand
	{
		private Action<Object> _action;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_action(parameter);
		}

		public DelegateCommand(Action<Object> action)
		{
			_action = action;
		}
	}
}
