using System;
using System.Threading.Tasks;
using cameratesteasy.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Camera_iOS))]
namespace cameratesteasy.iOS
{
	public class Camera_iOS : ICamera
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

		class CameraDelegate : UIImagePickerControllerDelegate
		{
			public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
			{
				var cb = _callback;
				_callback = null;

				picker.DismissModalViewController(true);
				cb(info);
			}
		}

		public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.Camera;
			_callback = callback;
			parent.PresentModalViewController(picker, true);
		}

		public static void SelectPicture(UIViewController parent, Action<NSDictionary> callback)
		{
			Init();
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			_callback = callback;
			parent.PresentModalViewController(picker, true);
		}

		public async Task TakePicture()
		{
			var rc = UIApplication.SharedApplication.KeyWindow.RootViewController;

			TakePicture(rc, (obj) =>
			{
				var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

				// hardcoded filename, overwritten each time
				string jpgFilename = System.IO.Path.Combine(documentsDirectory, "Photo.jpg");
				NSData imgData = photo.AsJPEG();
				NSError err = null;
				if (imgData.Save(jpgFilename, false, out err))
				{
					Console.WriteLine("saved as " + jpgFilename);
				}
				else
				{
					Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
				}

				imgData.Dispose();
				jpgFilename = null;
				photo.Dispose();
			});

			rc.Dispose();
		}
	}
}