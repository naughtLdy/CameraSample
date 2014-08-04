using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;


namespace CameraSample.Utils
{
	internal class BitmapToBitmapFrame
	{
		internal static BitmapFrame Convert(Bitmap src)
		{
			using (var stream = new MemoryStream())
			{
				src.Save(stream, ImageFormat.Bmp);
				return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
			}
		}

	}

}
