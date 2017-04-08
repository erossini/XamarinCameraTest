using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Plugin.Media;
using Xamarin.Forms;
using System.Threading;
using System.Diagnostics;

namespace cameratest
{
	public class BaseGalleryImage : BaseViewModel
	{
		/// <summary>
		/// The initialized
		/// </summary>
		private bool initialized = false;

		public ICommand _cameraCommand = null;
		public ICommand _previewImageCommand = null;
		public ICommand _saveCommand = null;
		public ImageSource _previewImage = null;

		/// <summary>
		/// The delete command
		/// </summary>
		public ICommand _deleteCommand = null;

		/// <summary>
		/// The pick command
		/// </summary>
		public ICommand _pickCommand = null;

		/// <summary>
		/// Gets or sets the images.
		/// </summary>
		/// <value>The images.</value>
		public ObservableCollection<GalleryImage> _images { get; set; } = new ObservableCollection<GalleryImage>();

		public ObservableCollection<GalleryImage> Images
		{
			get
			{
				return _images;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:myInspection.BaseGalleryImage"/> class.
		/// </summary>
		public BaseGalleryImage()
		{
		}

		/// <summary>
		/// Gets or sets the is loading.
		/// </summary>
		/// <value>The is loading.</value>
		public bool IsLoading { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:myInventories.ViewModels.BaseGalleryImage"/> can take
		/// new photo.
		/// </summary>
		/// <value><c>true</c> if can take new photo; otherwise, <c>false</c>.</value>
		public bool CanTakeNewPhoto
		{
			get
			{
				return _canTakePhoto;
			}
			set
			{
				if (_canTakePhoto != value)
				{
					_canTakePhoto = value;
					OnPropertyChanged("CanTakeNewPhoto");
				}
			}
		}
		private bool _canTakePhoto = true;

		/// <summary>
		/// Gets or sets the number of images.
		/// </summary>
		/// <value>The number of images.</value>
		public int NumberOfImages
		{
			get
			{
				return _numImages;
			}
			set
			{
				if (_numImages != value)
				{
					_numImages = value;
					OnPropertyChanged("NumberOfImages");
				}
			}
		}
		private int _numImages = 0;

		#region Gallery Images
		/// <summary>
		/// The show delete
		/// </summary>
		private bool _showDelete = false;

		/// <summary>
		/// Gets or sets a value indicating whether it's possible to show delete button.
		/// </summary>
		/// <value><c>true</c> if [show delete]; otherwise, <c>false</c>.</value>
		public bool ShowDelete
		{
			get
			{
				return _showDelete;
			}
			set
			{
				if (_showDelete != value)
				{
					_showDelete = value;
					OnPropertyChanged("ShowDelete");
				}
			}
		}

		/// <summary>
		/// Gets or sets the preview identifier.
		/// </summary>
		/// <value>The preview identifier.</value>
		public Guid PreviewId { get; set; }

		/// <summary>
		/// Gets or sets the preview image.
		/// </summary>
		/// <value>The preview image.</value>
		public ImageSource PreviewImage
		{
			get { return _previewImage; }
			set
			{
				SetProperty(ref _previewImage, value);
			}
		}

		/// <summary>
		/// Gets the camera command.
		/// </summary>
		/// <value>The camera command.</value>
		public ICommand CameraCommand
		{
			get { return _cameraCommand ?? new Command(async () => await ExecuteCameraCommand(), () => CanExecuteCameraCommand()); }
		}

		/// <summary>
		/// Determines whether this instance can execute camera command.
		/// </summary>
		/// <returns><c>true</c> if this instance can execute camera command; otherwise, <c>false</c>.</returns>
		public bool CanExecuteCameraCommand()
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Executes the camera command.
		/// </summary>
		/// <returns>The camera command.</returns>
		public async Task ExecuteCameraCommand()
		{
			IsBusy = true;

			try
			{
				ChangeMediaButtonEnableState(enable: false);

				CameraHelpers ch = new CameraHelpers();
				GalleryImage gi = await ch.TakePictureFromCamera();
				if (gi != null)
				{
					//for (int i = 0; i < 10; i++)
					_images.Add(gi);
				}
				ch.Dispose();
				ch = null;

				IsLoading = false;
				IsBusy = false;
			}
			catch (Exception ex)
			{
			}
			finally
			{
				ChangeMediaButtonEnableState(enable: true);
			}
		}

		/// <summary>
		/// Changes the state of the media button enable.
		/// </summary>
		/// <param name="enable">If set to <c>true</c> enable.</param>
		private void ChangeMediaButtonEnableState(bool enable)
		{
			NumberOfImages = _images.Count;
			CanTakeNewPhoto = enable;
		}

		/// <summary>
		/// Gets the preview image command.
		/// </summary>
		/// <value>The preview image command.</value>
		public ICommand PreviewImageCommand
		{
			get
			{
				return _previewImageCommand ?? new Command<Guid>((img) =>
				{
					if (_images.Count > 0)
					{
						var image = _images.FirstOrDefault(x => x.ImageId == img);
						if (image != null)
						{
							IsBusy = true;
							PreviewId = img;
							PreviewImage = ImageSource.FromFile(image.FilePath);
							ShowDelete = true;
							IsBusy = false;
						}
					}
				});
			}
		}

		/// <summary>
		/// Gets the camera command.
		/// </summary>
		/// <value>The camera command.</value>
		public ICommand PickCommand
		{
			get { return _pickCommand ?? new Command(async () => await ExecutePickCommand(), () => CanExecutePickCommand()); }
		}

		/// <summary>
		/// Determines whether this instance can execute camera command.
		/// </summary>
		/// <returns><c>true</c> if this instance can execute camera command; otherwise, <c>false</c>.</returns>
		public bool CanExecutePickCommand()
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Executes the camera command.
		/// </summary>
		/// <returns>The camera command.</returns>
		public async Task ExecutePickCommand()
		{
			try
			{
				ChangeMediaButtonEnableState(enable: false);

				CameraHelpers ch = new CameraHelpers();
				GalleryImage gi = await ch.PickPicture();
				if (gi != null)
				{
					_images.Add(gi);
				}
				ch.Dispose();
				ch = null;
			}
			catch (Exception ex)
			{
			}
			finally
			{
				ChangeMediaButtonEnableState(enable: true);
			}
		}
		#endregion
	}
}