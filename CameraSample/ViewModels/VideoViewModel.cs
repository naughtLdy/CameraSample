using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CameraSample.Models;
using CameraSample.Delegates;

namespace CameraSample.ViewModels
{
	class VideoViewModel : INotifyPropertyChanged
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

		private ImageSource _imageSource;
		public ImageSource ImageSource
		{
			get { return _imageSource; }
			set
			{
				OnPropertyChanged(ref _imageSource, value);
			}
		}

		private int _width = 960;
		public int Width
		{
			get { return _width; }
			set
			{
				OnPropertyChanged(ref _width, value);
			}
		}

		private int _height = 720;
		public int Height
		{
			get { return _height; }
			set
			{
				OnPropertyChanged(ref _height, value);
			}
		}


		public Camera Camera { get; set; }

		public ICommand FileSaveCommand { get; private set; }


		public VideoViewModel()
		{
			Camera = Camera.Instance;

			FileSaveCommand = new DelegateCommand(_ =>
			{
				try
				{
					using (var fs = new FileStream("SampleImage.tif", FileMode.Create))
					{
						var encoder = new TiffBitmapEncoder();
						encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageSource));
						encoder.Save(fs);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Error : " + e);
				}
			});
		}
	}
}
