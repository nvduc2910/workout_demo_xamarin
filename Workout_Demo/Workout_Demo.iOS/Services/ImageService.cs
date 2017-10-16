using System;
using System.Threading.Tasks;
using Foundation;
using WorkoutDemo.Core;
using UIKit;

namespace WorkoutDemo.iOS
{
	public class ImageService : IImageService
	{
		UIImagePickerController imagePicker;

		#region IImageService implementation

		public byte[] ScaleImage(byte[] imageData, int size)
		{
			var img = imageData.ToImageData();
			var scaleImg = img.ScaleAndRotateImage(size);
			var jpegData = scaleImg.AsJPEG(0.5f);
			return jpegData.ToArray();
		}

		public Task<byte[]> TakePicture()
		{
			var tcs = new TaskCompletionSource<byte[]>();

			var viewController = ((AppDelegate)UIApplication.SharedApplication.Delegate).Presenter.GetTopMostViewController();

			TweetStation.Camera.TakePicture(viewController, (obj) =>
			{

				UIImage originalImage = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				if (originalImage != null)
				{
				// do something with the image
				//                  Console.WriteLine ("got the original image");

				var scaleImage = originalImage.ScaleAndRotateImage(3000);
				//                  var scaleImage = originalImage.SquareImageAndScaleToSize (1080);
				var jpegData = scaleImage.AsJPEG(0.5f);

				//var bytes = originalImage.ToNSData ();
				originalImage.Dispose();
					originalImage = null;
				//scaleImage.Dispose();
				//scaleImage = null;

				var buffer = jpegData.ToArray();
					jpegData.Dispose();
					jpegData = null;

				//var buffer = originalImage.ToNSData();
				//var buffer = scaleImage.ToNSData();

				scaleImage.Dispose();
					scaleImage = null;

					tcs.TrySetResult(buffer);


				//tcs.TrySetResult (originalImage.FromNative ());
			}
			});

			return tcs.Task;

		}

		public Task<byte[]> SelectFromLibrary()
		{
			var tcs = new TaskCompletionSource<byte[]>();

			var viewController = ((AppDelegate)UIApplication.SharedApplication.Delegate).Presenter.GetTopMostViewController();

			TweetStation.Camera.SelectPicture(viewController, (obj) =>
			{
				UIImage originalImage = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				if (originalImage != null)
				{
				// do something with the image
				Console.WriteLine("got the original image");

					NSData jpegData;
					var scaleImage = originalImage.ScaleAndRotateImage(3000);
				//var scaleImage = originalImage.SquareImageAndScaleToSizeLibrary (1080);
				jpegData = scaleImage.AsJPEG(0.5f);

				//scaleImage.Dispose ();
				//scaleImage = null;
				//originalImage.Dispose ();
				//originalImage = null;

				var buffer = jpegData.ToArray();
					jpegData.Dispose();
					jpegData = null;

				//var buffer = originalImage.ToNSData();
				//var buffer = scaleImage.ToNSData();

				scaleImage.Dispose();
					scaleImage = null;

					tcs.TrySetResult(buffer);
				}
			});

			return tcs.Task;
		}

		void ImagePicker_Canceled(object sender, EventArgs e)
		{
			imagePicker.DismissViewController(true, () =>
			{
				imagePicker.Dispose();
			});
		}

		public Task<string> SaveImage(string imageName, byte[] data)
		{
			var tcs = new TaskCompletionSource<string>();

			var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, imageName);


			NSData imgData = ExtensionsImage.ToImageData(data).AsJPEG();

			NSError err = null;
			if (imgData.Save(jpgFilename, false, out err))
			{
				tcs.TrySetResult(jpgFilename);
				Console.WriteLine("saved as " + jpgFilename);
			}
			else
			{
				tcs.TrySetResult(null);
				Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
			}

			return tcs.Task;
		}

		public Task<bool> DeleteImage(string imageName)
		{
			var tcs = new TaskCompletionSource<bool>();

			var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, imageName);
			System.IO.File.Delete(jpgFilename);

			tcs.TrySetResult(true);

			return tcs.Task;
		}

		public byte[] GetByteFromImage(string path)
		{
			var image = UIImage.FromFile(path);
			if (image == null)
			{
				return null;
			}
			NSData data = null;

			try
			{
				data = image.AsJPEG();
				return data.ToArray();
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				if (image != null)
				{
					image.Dispose();
					image = null;
				}
				if (data != null)
				{
					data.Dispose();
					data = null;
				}
			}
		}
		#endregion

	}

	public static class ExtensionsImage
	{

		public static byte[] ToNSData(this UIImage image)
		{

			if (image == null)
			{
				return null;
			}
			NSData data = null;

			try
			{
				data = image.AsPNG();
				return data.ToArray();
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				if (image != null)
				{
					image.Dispose();
					image = null;
				}
				if (data != null)
				{
					data.Dispose();
					data = null;
				}
			}
		}

		public static UIImage ToImageData(this byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			UIImage image = null;
			try
			{

				image = new UIImage(NSData.FromArray(data));
				data = null;
			}
			catch (Exception)
			{
				return null;
			}
			return image;
		}
	}
}
