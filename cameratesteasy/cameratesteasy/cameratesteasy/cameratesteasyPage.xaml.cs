using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace cameratesteasy
{
	public partial class cameratesteasyPage : ContentPage
	{
		int count = 0;
		int nativecount = 0;

		public cameratesteasyPage()
		{
			InitializeComponent();
			CrossMedia.Current.Initialize();

			CountNativeLabel.Text = "0";
		}

		async void CameraNativeClick(object sender, System.EventArgs args)
		{
			await DependencyService.Get<ICamera>().TakePicture();
			nativecount++;
			CountNativeLabel.Text = $"{nativecount} times";
		}

		void UpdateCount()
		{
			count++;
			CountLabel.Text = $"{count} times";
		}

		async void StartCameraTapped(object sender, System.EventArgs args)
		{
			var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
			});

			if (file == null)
				return;

			UpdateCount();
		}

		async void StartCameraTakeTapped(object sender, System.EventArgs args)
		{
			var file = await CrossMedia.Current.PickPhotoAsync();

			if (file == null)
				return;

			UpdateCount();
		}
	}
}