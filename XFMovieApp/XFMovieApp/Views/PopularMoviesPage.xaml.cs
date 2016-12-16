using System;
using System.Collections.Generic;
using MovieApp.Models;
using XFMovieApp.Services;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;
using System.IO;
using Newtonsoft.Json;

namespace XFMovieApp
{
	public partial class PopularMoviesPage : ContentPage
	{
		private MovieService _service;
		List<MovieDetailsDTO> _movies;
		private FlowListView _list;
		private ActivityIndicator _spinner;


		public PopularMoviesPage()
		{
			InitializeComponent();
			_spinner = this.FindByName<ActivityIndicator>("spinner");
			_service = new MovieService();
			_movies = new List<MovieDetailsDTO>();
			_list = this.FindByName<FlowListView>("flowList");
			_list.RefreshCommand = new Command(() =>
			{
				GetPopularMovies();
			});
			GetPopularMovies();
		}

		private async void GetPopularMovies()
		{
			_spinner.IsVisible = true;
			_movies = await _service.PopularMovies();

			this.BindingContext = _movies;
			_spinner.IsVisible = false;
			_list.EndRefresh();
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
