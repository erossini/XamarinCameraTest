using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace cameratest
{
	public partial class cameratestPage : ContentPage
	{
		int count = 0;
		cameratestViewModel vm = null;

		public cameratestPage()
		{
			InitializeComponent();
			LoadData();

			CrossMedia.Current.Initialize();
		}

		void LoadData()
		{
			if (vm == null)
			{
				//vm = new cameratestViewModel();
				//BindingContext = vm;
			}
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