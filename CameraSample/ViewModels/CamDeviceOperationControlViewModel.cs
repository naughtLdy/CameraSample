using CameraSample.Delegates;
using CameraSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CameraSample.ViewModels
{
	class CamDeviceOperationControlViewModel : INotifyPropertyChanged
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

		public Camera Camera { get; set; }


		public ICommand ConnectCommand { get; private set; }
		public ICommand DisconnectCommand { get; private set; }



		public CamDeviceOperationControlViewModel()
		{
			Camera = Camera.Instance;

			ConnectCommand = new DelegateCommand(_ =>
			{
				var rtn = Camera.Start();
				if (rtn == false)
				{
					MessageBox.Show("デバイスを選択してください");
				}
			});

			DisconnectCommand = new DelegateCommand(_ =>
			{
				Task.Run(() =>
				{
					// TODO イベントリムーブ
					Camera.Stop();
				});
			});
		}
	}
}
