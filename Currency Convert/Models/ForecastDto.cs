namespace Currency_Convert.Models
{
  public class ForecastDto
  {
    public CurrentDto Current { get; set; } = new CurrentDto();

    public HourlyDto Hourly { get; set; } = new HourlyDto();
    public DailyDto Daily { get; set; } = new DailyDto();
  }
}
