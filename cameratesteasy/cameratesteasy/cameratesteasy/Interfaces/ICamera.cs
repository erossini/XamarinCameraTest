using System;
using System.Threading.Tasks;

namespace cameratesteasy
{
	public interface ICamera
	{
		Task TakePicture();
	}
}