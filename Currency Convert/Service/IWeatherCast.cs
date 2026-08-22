using Currency_Convert.Models;

namespace Currency_Convert.Service
{
  public interface IWeatherCast
  {
    //Task<List<WorldCity> GetCityCountiesAsync(string cityName);
    Task<ForecastDto> GetWeatherAsync(decimal Latitude, decimal Longitude);
  }
}
