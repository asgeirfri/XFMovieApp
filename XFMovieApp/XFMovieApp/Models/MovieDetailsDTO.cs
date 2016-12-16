using System;
using System.Collections.Generic;

namespace MovieApp.Models
{
	public class MovieDetailsDTO
	{
		public DM.MovieApi.MovieDb.Movies.MovieInfo info { get; set; }
		public string poster { get; set; }
		public string casts { get; set; }
		public string duration { get; set; }
		public string title { get; set; }
		public string genreString { get; set; }
		public string imageUrl { get; set; }

		public MovieDetailsDTO(DM.MovieApi.MovieDb.Movies.MovieInfo info, string poster, string casts, string duration)
		{
			this.info = info;
			this.poster = poster;
			this.casts = casts;
			this.duration = duration;
			if (info != null)
			{
				this.title = this.info.Title + " (" + this.info.ReleaseDate.Year + ")";
				this.genreString = duration + " min | ";
				foreach (var genre in info.Genres)
				{
					this.genreString += genre.Name + ", ";
				}
				if (info.Genres.Count != 0)
				{
					genreString = genreString.Remove(genreString.Length - 2);
				}
				imageUrl = "http://image.tmdb.org/t/p/w185" + info.PosterPath;
			}
		}

		public override string ToString()
		{
			return this.info.Title + " (" + this.info.ReleaseDate.Year + ")";
		}
	}
}
