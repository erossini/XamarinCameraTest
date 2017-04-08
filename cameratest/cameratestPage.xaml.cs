using Xamarin.Forms;

namespace cameratest
{
	public partial class cameratestPage : ContentPage
	{
		cameratestViewModel vm = null;

		public cameratestPage()
		{
			InitializeComponent();
			LoadData();
		}

		void LoadData()
		{
			if (vm == null)
			{
				vm = new cameratestViewModel();
				BindingContext = vm;
			}
		}
	}
}