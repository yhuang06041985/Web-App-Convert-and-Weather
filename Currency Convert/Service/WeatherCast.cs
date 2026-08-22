using Currency_Convert.Data;
using Currency_Convert.Models;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Currency_Convert.Components.Pages.Converter;


namespace Currency_Convert.Service
{
  public class WeatherCast : IWeatherCast
  {
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _dbContext;
    private WeatherResponse _response;

    private IDataFormat _dataFormat;
    public WeatherCast(HttpClient httpClient, AppDbContext dbContext, IDataFormat dataFormat)
    {
      _httpClient = httpClient;
      _dbContext = dbContext;
      _dataFormat = dataFormat;
    }


   /* public async Task<List<WorldCity>> GetCityCountiesAsync(string cityName)
    {
      List<WorldCity> cityCounties = _dbContext.WorldCitics
        .Where(c => c.City.Equals(cityName, StringComparison.OrdinalIgnoreCase))
        .ToList();
      return cityCounties;  
    }*/

    public async Task<ForecastDto?> GetWeatherAsync(
            decimal latitude,
            decimal longitude)
    {
      string apiUrl =
          $"https://api.open-meteo.com/v1/forecast" +
          $"?latitude={latitude}" +
          $"&longitude={longitude}" +
          $"&current=temperature_2m,apparent_temperature,weather_code,relative_humidity_2m,wind_speed_10m" +
          $"&hourly=temperature_2m,apparent_temperature,weather_code,relative_humidity_2m,wind_speed_10m" +
          $"&daily=weather_code,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,precipitation_probability_max,precipitation_sum,sunrise,sunset" +
          $"&temperature_unit=fahrenheit" +
          $"&wind_speed_unit=mph" +
          $"&timezone=auto" +
          $"&forecast_days=7";

      ForecastDto forecastDto = new ForecastDto();

      try
      {
        var weatherResponse =
            await _httpClient.GetFromJsonAsync<WeatherResponse>(apiUrl);

        if (weatherResponse != null)
        {
          // Format the weather data and populate the forecast DTO
          // This is a simplified example - you would need to implement the actual formatting logic
          forecastDto.Current = _dataFormat.FormatCurrentData(weatherResponse.Current, forecastDto.Current);
          forecastDto.Hourly = _dataFormat.FormatHourlyData(weatherResponse.Hourly, forecastDto.Hourly);
          forecastDto.Daily = _dataFormat.FormatDailyData(weatherResponse.Daily, forecastDto.Daily);
        }

          return forecastDto;
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine(
            $"Weather API request failed: {ex.Message}");

        return null;
      }
      catch (Exception ex)
      {
        Console.WriteLine(
            $"Unexpected weather error: {ex.Message}");

        return null;
      }
    }

    /*public async Task<List<Location>> GetCityCountiesAsync2(string cityName)
    {
      List<Location> cityCounties = new List<Location>();
      try
      {
        //string apiUrl = $"https://nominatim.openstreetmap.org/search?q={cityName}&format=json&addressdetails=1&limit=5";
        //cityCounties = await _httpClient.GetFromJsonAsync<List<CityCounty>>(apiUrl);
        string encodedCity = WebUtility.UrlEncode(cityName);
        string apiUrl = $"https://nominatim.openstreetmap.org/search?q={encodedCity}&format=json&addressdetails=1&limit=5&email=your-email@example.com";
        _response = await _httpClient.GetFromJsonAsync<List<WeatherResponse>>(apiUrl);

        if (cityCounties == null || cityCounties.Count == 0)
        {
          Console.WriteLine($"No results found for city: {cityName}");
        }
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine($"Http request error: {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Unexpected error: {ex.Message}");
      }
      return cityCounties;
    }*/
  }
}
