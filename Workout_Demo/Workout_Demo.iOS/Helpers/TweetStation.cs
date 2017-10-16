using System;
using Foundation;
using MobileCoreServices;
using UIKit;

namespace TweetStation
{
	public static class Camera
	{
		static UIImagePickerController picker;
		static Action<NSDictionary> _callback;

		static void Init()
		{
			if (picker != null)
				return;

			picker = new UIImagePickerController();
			picker.Delegate = new CameraDelegate();
		}

		static void OverlayView_BtnTakePhoto_TouchUpInside(object sender, EventArgs e)
		{
			picker.TakePicture();
		}

		static void OverlayView_BtnClose_TouchUpInside(object sender, EventArgs e)
		{
			picker.DismissViewController(true, null);

		}

		class CameraDelegate : UIImagePickerControllerDelegate
		{
			public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
			{
				var cb = _callback;
				_callback = null;

				picker.DismissViewController(true, null);
				cb(info);
			}
		}

		public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.Camera;
			//          picker.ShowsCameraControls = false;
			//          picker.CameraOverlayView = overlayView.View;
			_callback = callback;
			parent.PresentViewController(picker, true, null);
		}

		public static void SelectPicture(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			_callback = callback;
			parent.PresentViewController(picker, true, null);
		}


		public static void SelectFromCameraRoll(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.SavedPhotosAlbum;
			_callback = callback;
			parent.PresentViewController(picker, true, null);
		}

		public static void SelectVideoFromLibrary(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			picker.MediaTypes = new string[] { UTType.Movie };
			_callback = callback;
			parent.PresentViewController(picker, true, null);
		}

	}
}
