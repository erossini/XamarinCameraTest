using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Media;

namespace cameratest
{
	public class CameraHelpers : IDisposable
	{
		private Guid _id;
		private bool initialized = false;
		private Task feedTask;

		// prevent that a user invoked Take and Pick Photo event at same time
		private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

		public CameraHelpers()
		{
			_id = new Guid();
		}

		public CameraHelpers(Guid Id)
		{
			_id = Id;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task InitMedia()
		{
			await CrossMedia.Current.Initialize();
			initialized = true;
		}

		public async Task<GalleryImage> TakePictureFromCamera()
		{
			GalleryImage rtn = null;

			if (!initialized)
				await InitMedia();

			//await _semaphoreSlim.WaitAsync();
			try
			{
				if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
				{
					using (await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
					{
						Name = $"{_id.ToString()}.jpg",
					}))
					{
						//if (file != null)
						//rtn = new GalleryImage() { ImageId = _id, FilePath = file.Path };
					}
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				//_semaphoreSlim.Release();
			}

			return rtn;
		}

		public async Task<GalleryImage> PickPicture()
		{
			GalleryImage rtn = null;

			if (!initialized)
				await InitMedia();

			await _semaphoreSlim.WaitAsync();
			try
			{
				if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
				{
					using (var file = await CrossMedia.Current.PickPhotoAsync())
					{
					}
				}
			}
			catch (Exception ex)
			{
			}
			finally
			{
				_semaphoreSlim.Release();
			}

			return rtn;
		}
	}
}