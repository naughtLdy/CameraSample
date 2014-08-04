using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraSample.Models
{
	class Camera
	{
		private static Camera m_Instance;

		private event NewFrameEventHandler Event;

		private VideoCaptureDevice device;

		private Camera()
		{
			Event = delegate { };
		}

		public static Camera Instance
		{
			get { return m_Instance ?? (m_Instance = new Camera()); }
		}

		/// <summary>
		/// 解像度設定
		/// </summary>
		/// <param name="index"></param>
		public void Resolution(int index)
		{
			device.VideoResolution = device.VideoCapabilities[index];
		}

		public VideoCapabilities[] videoCapabilities()
		{
			return device.VideoCapabilities;
		}

		/// <summary>
		/// イベントの追加
		/// </summary>
		/// <param name="EventHandler"></param>
		public void SetHandler(NewFrameEventHandler EventHandler)
		{
			Event += EventHandler;
		}

		/// <summary>
		/// イベントの削除
		/// </summary>
		/// <param name="EventHandler"></param>
		public void RemoveHandler(NewFrameEventHandler EventHandler)
		{
			Event -= EventHandler;
		}

		/// <summary>
		/// デバイス情報設定
		/// デバイス情報にイベント追加
		/// </summary>
		/// <param name="DeviceName"></param>
		public void SetDevice(string DeviceName)
		{
			device = new VideoCaptureDevice(DeviceName);
			device.NewFrame += Event;
		}


		/// <summary>
		/// カメラ起動
		/// </summary>
		/// <returns></returns>
		public bool Start()
		{
			if ((device == null) || (device.Source == null))
			{
				return false;
			}

			device.NewFrame += Event;
			device.Start();
			return true;
		}

		/// <summary>
		/// カメラ終了
		/// </summary>
		public void Stop()
		{
			device.SignalToStop();
			device.Stop();
		}
	}
}
