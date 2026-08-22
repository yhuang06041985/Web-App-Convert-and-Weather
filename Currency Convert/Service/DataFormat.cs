using Currency_Convert.Models;
using Currency_Convert.Service;
using Currency_Convert.Data;
using Microsoft.EntityFrameworkCore;

namespace Currency_Convert.Service
{

  public class DataFormat : IDataFormat
  {
    private readonly AppDbContext _context;
    private List<WeatherCode> weatherCodes;
    private List<TimeHour> timeHourCodes;

    private WeatherCode weatherCode;
    private TimeHour timeHour;
    public CurrentDto FormatCurrentData(Current currentWeather, CurrentDto currentData)
    {

      weatherCode = GetWeatherCode(currentWeather.weather_code);
      currentData.Temperature = currentWeather.temperature_2m + "°F";
      currentData.TemperatureLike = "Feel likes " + currentWeather.apparent_temperature + "°F";
      currentData.WindSpeed = currentWeather.wind_speed_10m.ToString() + " mph";
      currentData.Description = weatherCode.Description?? string.Empty;
      currentData.IconUrl = weatherCode.IconUrl?? string.Empty;

      return currentData;
    }

    public HourlyDto FormatHourlyData(Hourly hourlyWeather, HourlyDto hourlyData)
    {
      string  dateFromApi;
      for (int i=0; i< hourlyData.Time.Count; i++)
      {
        weatherCode = GetWeatherCode(hourlyWeather.weather_code[i]);
        hourlyData.Temperature[i] = hourlyWeather?.temperature_2m[i] + "°F";
        hourlyData.IconUrl[i] =  weatherCode.IconUrl?? string.Empty;
        dateFromApi = hourlyWeather.time[i];
        hourlyData.Time[i] = ToComplicateTime(dateFromApi);
      }
     
      return hourlyData;
    }

    public DailyDto FormatDailyData(Daily dailyWeather, DailyDto dailyData)
    {
      string dateFromApi;
      for (int i = 0; i < dailyData.Time.Count; i++)
      {
        weatherCode = GetWeatherCode(dailyWeather.weather_code[i]);
        dailyData.TemperatureHigh[i] = dailyWeather?.temperature_2m_max[i] + "°F";
        dailyData.TemperatureLow[i] = dailyWeather?.temperature_2m_min[i] + "°F";
        dailyData.IconUrl[i] = weatherCode.IconUrl ?? string.Empty;
        dateFromApi = dailyWeather.time[i];
        dailyData.Time[i] = ToShortDay(dateFromApi);
      }

      return dailyData;
    }

    private void LoadWeatherCodes()
    {
      weatherCodes = _context.WeatherCodes.AsNoTracking().ToList();
    }

    private void LoadTimeHourCodes()
    {
      timeHourCodes = _context.TimeHours.AsNoTracking().ToList();
    }

    private WeatherCode GetWeatherCode(int code)
    {
      return weatherCodes.FirstOrDefault(c => c.Code == code)?? new WeatherCode();
    }

    private TimeHour GetTimeHour(int hour)
    {
      return timeHourCodes.FirstOrDefault(h => h.Hour == hour)?? new TimeHour();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">  2026-08-20T06:00  </param>
    /// <returns> Thursday, August 20 </returns>
    private string ToComplicateDay(string input)
    {
      var index = input.IndexOf("T");
      input = input.Substring(0, index);
      DateTime dt = DateTime.Parse(input);
      return dt.ToString("dddd, MMMM d");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">   2026-08-20T06:00  </param></param>
    /// <returns> Today, Mon, Tue </returns>
    private string ToShortDay(string input)
    {

      var index = input.IndexOf("T");
      input = input.Substring(0, index);
      DateTime dt = DateTime.Parse(input).Date;

      if (dt == DateTime.Today)
        return "Today";

      return dt.ToString("ddd");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">  2026-08-20T06:00   </param>
    /// <returns>  6 AM; 8 PM </returns>
    private string ToComplicateTime(string input)
    {
      var index = input.IndexOf("T") + 1;
      input = input.Substring(index);

      DateTime dt = DateTime.Parse(input);
      return dt.ToString("h tt");
    }

  }
}
