using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieApp.Models;
using Xamarin.Forms;

namespace XFMovieApp
{
    public class App : Application
    {
        public App()
        {
			// The root page of your application
			var movieSearchPage = new MovieSearchPage();
			var movieSearchNavigationPage = new NavigationPage(movieSearchPage)
			{
				Title = "Search",
				Icon = "SearchFilled.png",
				BackgroundColor = Color.FromHex("d61616")
			};

			var topMoviesPage = new TopMoviesPage();
			var topMoviesNavigationPage = new NavigationPage(topMoviesPage)
			{
				Title = "Top rated",
				Icon = "Star.png",
				BackgroundColor = Color.FromHex("d61616")
			};

			var popularMoviesPage = new PopularMoviesPage();
			var popularMoviesNavigationPage = new NavigationPage(popularMoviesPage)
			{
				Title = "Popular",
				Icon = "Popular.png",
				BackgroundColor = Color.FromHex("d61616")
			};

			var tabbedPage = new TabbedPage();
			tabbedPage.Children.Add(movieSearchNavigationPage);
			tabbedPage.Children.Add(topMoviesNavigationPage);
			tabbedPage.Children.Add(popularMoviesNavigationPage);

			this.MainPage = tabbedPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
