using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XFMovieApp
{
	public partial class MovieListPage : ContentPage
	{
		public MovieListPage()
		{
			InitializeComponent();
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
