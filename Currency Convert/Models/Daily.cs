using System.Timers;

namespace Currency_Convert.Models
{
  public class Daily
  {
    public List<string> time { get; set; } = new();
    public List<int> weather_code { get; set; } = new();
    public List<double> temperature_2m_max { get; set; } = new();
    public List<double> temperature_2m_min { get; set; } = new();
  }
}
