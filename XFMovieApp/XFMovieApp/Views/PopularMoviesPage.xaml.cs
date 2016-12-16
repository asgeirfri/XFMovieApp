using System;
using System.Collections.Generic;
using MovieApp.Models;
using XFMovieApp.Services;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;

namespace XFMovieApp
{
	public partial class PopularMoviesPage : ContentPage
	{
		private MovieService _service;
		List<MovieDetailsDTO> _movies;
		private FlowListView _flowListView;


		public PopularMoviesPage()
		{
			InitializeComponent();
			_service = new MovieService();
			_movies = new List<MovieDetailsDTO>();
			GetPopularMovies();
			_flowListView = new FlowListView();
		}

		private async void GetPopularMovies()
		{
			var _spinner = this.FindByName<ActivityIndicator>("spinner");
			//var _list = this.FindByName<ListView>("listview");
			//_list.IsVisible = false;
			_spinner.IsVisible = true;

			_movies = await _service.PopularMovies();

			this.BindingContext = _movies;

			//this.FindByName<ListView>("listview").ItemsSource = _movies;
			_spinner.IsVisible = false;
			//_list.IsVisible = true;
		}

				/*private async void Listview_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
				{
					if (e.SelectedItem == null)
					{
						return;
					}

					await this.Navigation.PushAsync(new MovieDetailPage() { BindingContext = e.SelectedItem });
				}*/
		
		public async void itemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
			{
				return;
			}

			await this.Navigation.PushAsync(new MovieDetailPage() { BindingContext = e.Item });
		}
	}
}
