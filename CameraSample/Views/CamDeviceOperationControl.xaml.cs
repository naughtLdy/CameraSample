using System;
using System.Collections.Generic;
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
using AForge.Video;
using AForge.Video.DirectShow;
using CameraSample.Models;
using CameraSample.ViewModels;

namespace CameraSample.Views
{
	/// <summary>
	/// CamDeviceOperationControl.xaml の相互作用ロジック
	/// </summary>
	public partial class CamDeviceOperationControl : UserControl
	{
		public CamDeviceOperationControl()
		{
			InitializeComponent();
		}

		private void deviceListCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CamDeviceOperationControlViewModel)DataContext).Camera.SetDevice((string)deviceListCombo.SelectedValue);

			// カメラの画像サイズ
			VideoCapabilities[] videoCapabilities = ((CamDeviceOperationControlViewModel)DataContext).Camera.videoCapabilities();
			foreach (VideoCapabilities capabilty in videoCapabilities)
			{
				ImageSize.Items.Add(string.Format("{0} x {1}", capabilty.FrameSize.Width, capabilty.FrameSize.Height));
			}
			ImageSize.SelectedIndex = 0;
		}

		private void ImageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CamDeviceOperationControlViewModel)DataContext).Camera.Resolution(ImageSize.SelectedIndex);
		}

	}
}
