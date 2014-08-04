using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraSample.ViewModels
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		private void OnPropertyChanged<T>(ref T name, T value, [CallerMemberName] string propertyName = null)
		{
			if (value == null)
			{
				name = value;
				OnPropertyChanged(propertyName);
				return;
			}
			if (value.Equals(name)) return;
			name = value;
			OnPropertyChanged(propertyName);
		}

		private string _title = "MainWindow";
		public string Title
		{
			get { return _title; }
			set
			{
				OnPropertyChanged(ref _title, value);
			}
		}


		public MainWindowViewModel()
		{

		}
	}
}
