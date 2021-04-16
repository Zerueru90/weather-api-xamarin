using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Text.Json;
using Weather.Models;

namespace Weather.Services
{
    //You replace this class witth your own Service from Project Part A
    public class OpenWeatherService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiKey = "9e7b864810f35ceb2ff6fa6ff02b7fe0"; // Your API Key
        
        public async Task<Forecast> GetForecastAsync(string City)
        {
            //https://openweathermap.org/current
            var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var uri = $"https://api.openweathermap.org/data/2.5/forecast?q={City}&units=metric&lang={language}&appid={apiKey}";

            WeatherApiData newWeatherApiData = null;
            //Your Code
            using (HttpResponseMessage response = await httpClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error: HTTP response was NOT successful");

                newWeatherApiData = await response.Content.ReadFromJsonAsync<WeatherApiData>();
            }

            var forecast = new Forecast
            {
                City = newWeatherApiData.city.name,
                Items = newWeatherApiData.list.Select(x => new ForecastItem
                {
                    DateTime = UnixTimeStampToDateTime(x.dt),
                    Temperature = x.main.temp,
                    WindSpeed = x.wind.speed,
                    Description = x.weather.Select(y => y.description).FirstOrDefault()
                }).ToList()
            };

            return forecast;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
