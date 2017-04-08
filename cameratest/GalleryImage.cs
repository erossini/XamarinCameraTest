using System;
using MvvmHelpers;

namespace cameratest
{
	public class GalleryImage : ObservableObject
	{
		public GalleryImage()
		{
			ImageId = Guid.NewGuid();
		}

		public int Id { get; set; }

		public int InspectionId { get; set; }

		public int RoomId { get; set; }

		public int OrderId { get; set; }

		public Guid ImageId { get; set; }

		//public ImageSource Source { get; set; }

		public byte[] OrgImage { get; set; }

		public string FilePath { get; set; }

		public string ThumbnailPath { get; set; }
	}
}