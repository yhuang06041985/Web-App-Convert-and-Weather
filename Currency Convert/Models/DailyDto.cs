using System.Timers;

namespace Currency_Convert.Models
{
  public class DailyDto
  {
    public List<string> Time   { get; set; }
    public List<string> IconUrl { get; set; }
    public List<string> TemperatureHigh { get; set; }
    public List<string> TemperatureLow { get; set; }  

  }
}
