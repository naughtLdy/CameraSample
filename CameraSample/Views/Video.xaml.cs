using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CameraSample.ViewModels;

namespace CameraSample.Views
{
	/// <summary>
	/// Video.xaml の相互作用ロジック
	/// </summary>
	public partial class Video : UserControl
	{
		public Video()
		{
			InitializeComponent();
			// カメラ画像取得イベント
			((VideoViewModel)DataContext).Camera.SetHandler(CamDeviceCtrlNewFrameGot);
		}


		/// <summary>
		/// カメラ画像表示イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void CamDeviceCtrlNewFrameGot(object sender, AForge.Video.NewFrameEventArgs eventArgs)
		{
			Bitmap imgforms = (Bitmap)eventArgs.Frame.Clone();
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();

			MemoryStream ms = new MemoryStream();
			imgforms.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
			ms.Seek(0, SeekOrigin.Begin);

			bi.StreamSource = ms;
			bi.EndInit();
			bi.Freeze();

			//Calling the UI thread using the Dispatcher to update the 'Image' WPF control         
			Dispatcher.BeginInvoke(new System.Threading.ThreadStart(delegate
			{
				((VideoViewModel)DataContext).ImageSource = bi;
			}));
		}
	}
}
