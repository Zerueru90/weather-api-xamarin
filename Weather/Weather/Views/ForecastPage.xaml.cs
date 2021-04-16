using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Weather.Models;
using Weather.Services;

namespace Weather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ForecastPage : ContentPage
    {
        OpenWeatherService service;
        GroupedForecast groupedforecast;
        List<Forecast> forecastList = new List<Forecast>();

        public ForecastPage()
        {
            InitializeComponent();
            
            service = new OpenWeatherService();
            groupedforecast = new GroupedForecast();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Code here will run right before the screen appears
            //You want to set the Title or set the City
            groupedforecast.City = Title;

            Title = $"Forecast for {Title}";

            //This is making the first load of data
            MainThread.BeginInvokeOnMainThread(async () => {await LoadForecast();});
        }

        private async Task LoadForecast()
        {
            //Heare you load the forecast 

            Forecast forecast = null;
            try
            {
                forecast = await service.GetForecastAsync(groupedforecast.City);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",$"Error message: {ex.Message}", "Cancel");
            }

            forecastList.Add(forecast);

            var item = forecastList.Select(x => x.Items).FirstOrDefault();
            var groupedItems = item.GroupBy(x => x.DateTime.ToString("dddd, MMMM dd, yyyy"));
            listViewPage.ItemsSource = groupedItems;

        }

        private async void BtnRefresh_Clicked(object sender, EventArgs e)
        {
            await LoadForecast();
        }
    }
}