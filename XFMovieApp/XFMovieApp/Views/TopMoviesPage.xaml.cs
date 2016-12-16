using System;
using System.Collections.Generic;
using MovieApp.Models;
using XFMovieApp.Services;
using Xamarin.Forms;

namespace XFMovieApp
{
	public partial class TopMoviesPage : ContentPage
	{
		private MovieService _service;
		List<MovieDetailsDTO> _movies;
		private ActivityIndicator _spinner;
		private ListView _list;

		public TopMoviesPage()
		{
			InitializeComponent();
			_service = new MovieService();
			_movies = new List<MovieDetailsDTO>();
			_spinner = this.FindByName<ActivityIndicator>("spinner");
			_list = this.FindByName<ListView>("listview");

			_list.RefreshCommand = new Command(() =>
			{
				GetTopMovies();
			});
			GetTopMovies();
		}

		private async void GetTopMovies()
		{
			_list.IsVisible = false;
			_spinner.IsVisible = true;

			_movies = await _service.TopMovies();

			this.BindingContext = _movies;

			//this.FindByName<ListView>("listview").ItemsSource = _movies;
			_spinner.IsVisible = false;
			_list.IsVisible = true;
			_list.EndRefresh();
		}

		private async void Listview_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return;
			}

			await this.Navigation.PushAsync(new MovieDetailPage() { BindingContext = e.SelectedItem });
		}
	}
}
