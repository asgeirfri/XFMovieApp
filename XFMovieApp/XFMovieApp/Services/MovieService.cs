using System;
using System.Collections.Generic;
using MovieApp.Models;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using DM.MovieApi;

namespace XFMovieApp.Services
{
	/*
	 * MovieService is a gateway to the MovieDB API 
	 */
	public class MovieService
	{
		private IPosterDownload _downloader;
		private IApiMovieRequest _movieApi;

		public MovieService()
		{
			MyMovieDbSettings settings = new MyMovieDbSettings();
			MovieDbFactory.RegisterSettings(settings);

			_downloader = null;
			_movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
		}

		public async Task<List<MovieDetailsDTO>> SearchMovies (string query)
		{
			ApiSearchResponse<MovieInfo> response = new ApiSearchResponse<MovieInfo>();
			var results = new List<MovieDetailsDTO>();
			if (String.IsNullOrEmpty(query))
			{
				return results;
			}
			if (_movieApi == null)
			{
				return new List<MovieDetailsDTO>();
			}
			response = await _movieApi.SearchByTitleAsync(query);
			if (response.Results == null)
			{
				return await SearchMovies(query);
			}
			foreach (var res in response.Results)
			{
				var resp = await GetMovieInfo(res);
				results.Add(resp);
			}
			return results;
		}

		public async Task<List<MovieDetailsDTO>> TopMovies()
		{
			ApiSearchResponse<MovieInfo> response;
			var results = new List<MovieDetailsDTO>();
			if (_movieApi == null)
			{
				return new List<MovieDetailsDTO>();
			}
			response = await _movieApi.GetTopRatedAsync();
			if (response.Results == null)
			{
				return await TopMovies();
			}
			foreach (var res in response.Results)
			{
				var resp = await GetMovieInfo(res);
				results.Add(resp);
			}
			return results;
		}

		public async Task<List<MovieDetailsDTO>> PopularMovies()
		{
			ApiSearchResponse<MovieInfo> response = new ApiSearchResponse<MovieInfo>();
			var results = new List<MovieDetailsDTO>();
			if (_movieApi == null)
			{
				return new List<MovieDetailsDTO>();
			}
			response = await _movieApi.GetPopularAsync();
			if (response.Results == null)
			{
				return await PopularMovies();
			}
			foreach (var res in response.Results)
			{
				var resp = await GetMovieInfo(res);
				results.Add(resp);
			}
			return results;
		}

		public async Task<MovieDetailsDTO> GetMovieInfo(MovieInfo res)
		{
			var durationResponse = await _movieApi.FindByIdAsync(res.Id);
			var creditResponse = await _movieApi.GetCreditsAsync(res.Id);
			string localPath = "";
			string casts = "";
			var duration = "";

			if (_downloader != null)
			{
				localPath = await _downloader.DownloadImage(res.PosterPath);
			}

			if (creditResponse.Item != null)
			{
				for (int i = 0; i < 3; i++)
				{
					if (creditResponse.Item.CastMembers.Count > i)
					{
						casts += creditResponse.Item.CastMembers[i].Name + ", ";
					}
				}
				if (casts.Length > 2)
				{
					casts = casts.Remove(casts.Length - 2);
				}
			}

			if (durationResponse.Item != null)
			{
				duration = durationResponse.Item.Runtime.ToString();
			}

			var resp = new MovieDetailsDTO(res, localPath, casts, duration);
			return resp;
		}
	}
}
