using System.Timers;

namespace Currency_Convert.Models
{
  public class Current
  {
    public string? time { get; set; }
    public decimal temperature_2m { get; set; }
    public decimal apparent_temperature { get; set; }
    public int weather_code { get; set; }
    public decimal wind_speed_10m { get; set; }

  }
}
