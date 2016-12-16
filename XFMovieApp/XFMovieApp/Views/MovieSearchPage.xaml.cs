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

		private Entry _nameEntry;

		private Button _displayMovieButton;

		private ActivityIndicator _spinner;

		private ListView _list;

		public MovieSearchPage()
		{
			InitializeComponent();
			this.Title = "Movie Search";
			_service = new MovieService();
			_nameEntry = this.FindByName<Entry>("nameEntry");
			_displayMovieButton = this.FindByName<Button>("displayMovieButton");
			_spinner = this.FindByName<ActivityIndicator>("spinner");
			_list = this.FindByName<ListView>("listview");
		}

		private async void OnDisplayMovieButtonClicked(object sender, EventArgs args)
		{
			_list.IsVisible = false;
			_spinner.IsVisible = true;
			this._displayMovieButton.IsEnabled = false;
			List<MovieDetailsDTO> movies = await _service.SearchMovies(this._nameEntry.Text);
			this._displayMovieButton.IsEnabled = true;
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
