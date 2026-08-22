using Currency_Convert.Models;

namespace Currency_Convert.Service
{
  public interface IDataFormat
  {
    CurrentDto FormatCurrentData(Current currentWeather, CurrentDto currentData);
    HourlyDto FormatHourlyData(Hourly hourlyWeather, HourlyDto hourlyData);
    DailyDto FormatDailyData(Daily dailyWeather, DailyDto dailyData);
  }
}
