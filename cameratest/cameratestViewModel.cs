using System;
using MvvmHelpers;
using Xamarin.Forms;

namespace cameratest
{
	public class cameratestViewModel : BaseGalleryImage
	{
		public cameratestViewModel()
		{
			MessagingCenter.Subscribe<MemoryMessage>(this, "MemoryMessage", message =>
			{
				Message = "I received a memory alert";
			});
		}

		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				if (_message != value)
				{
					_message = value;
					OnPropertyChanged("Message");
				}
			}
		}
		private string _message = "";
	}
}