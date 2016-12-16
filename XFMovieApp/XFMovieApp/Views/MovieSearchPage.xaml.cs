using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using MovieApp.Models;
using XFMovieApp.Services;
using Xamarin.Forms;

namespace XFMovieApp
{
	public partial class MovieSearchPage : ContentPage
	{
		private MovieService _service;

		private SearchBar _nameEntry;

		private ActivityIndicator _spinner;

		private ListView _list;

		public MovieSearchPage()
		{
			InitializeComponent();
			this.Title = "Movie Search";
			_service = new MovieService();
			_nameEntry = this.FindByName<SearchBar>("nameEntry");
			_spinner = this.FindByName<ActivityIndicator>("spinner");
			_list = this.FindByName<ListView>("listview");
			_nameEntry.SearchCommand = new Command(() =>
			{
				OnDisplayMovieButtonClicked(null, null);
			});
		}

		private async void OnDisplayMovieButtonClicked(object sender, EventArgs args)
		{
			_list.IsVisible = false;
			_spinner.IsVisible = true;
			List<MovieDetailsDTO> movies = await _service.SearchMovies(this._nameEntry.Text);
			this.BindingContext = movies;
			_spinner.IsVisible = false;
			_list.IsVisible = true;
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
