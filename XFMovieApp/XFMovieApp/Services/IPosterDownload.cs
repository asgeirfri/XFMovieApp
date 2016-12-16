using System;
using System.Threading.Tasks;

namespace XFMovieApp.Services
{
	public interface IPosterDownload
	{
		Task<string> DownloadImage(string path);
	}
}
