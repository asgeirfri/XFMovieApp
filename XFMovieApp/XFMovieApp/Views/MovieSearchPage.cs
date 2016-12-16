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
    public class MovieSearchPage : ContentPage
    {
		private MovieService _service;

        private Label _entryLabel = new Label
        {
            HorizontalOptions = LayoutOptions.Start,
            Text = "Enter words in a movie title:",
        };

        private Entry _nameEntry = new Entry
        {
            HorizontalOptions = LayoutOptions.Fill,
            Placeholder = "Movie title",
        };

        private Button _displayMovieButton = new Button
        {
            Text = "Get Movie",
            BorderColor = Color.Gray,
            HorizontalOptions = LayoutOptions.Fill,
        };

		private ActivityIndicator _spinner = new ActivityIndicator
		{
			Color = Color.Pink
		};

		public MovieSearchPage()
        {
			_service = new MovieService();
            this.BackgroundColor = Color.FromRgb(240, 240, 240);
            this.Title = "Movie Search";

            this.Content = new StackLayout
                               {
                                   Margin = 30,
                                   VerticalOptions = LayoutOptions.Start,
                                   Spacing = 10,
                                   Children =
                                       {
                                           new StackLayout { Children = { this._entryLabel, this._nameEntry, }, },
										   this._displayMovieButton,
										   this._spinner
                                       }
                               };

            this._displayMovieButton.Clicked += this.OnDisplayMovieButtonClicked;
            this._nameEntry.Completed += this.OnDisplayMovieButtonClicked;
        }

		private async void OnDisplayMovieButtonClicked(object sender, EventArgs args)
		{
			_spinner.IsRunning = true;
			this._displayMovieButton.IsEnabled = false;
			List<MovieDetailsDTO> movies = await _service.SearchMovies(this._nameEntry.Text);
			this._displayMovieButton.IsEnabled = true;
			await this.Navigation.PushAsync(new MovieListPage() { BindingContext = movies });
			_spinner.IsRunning = false;
        }
    }
}
