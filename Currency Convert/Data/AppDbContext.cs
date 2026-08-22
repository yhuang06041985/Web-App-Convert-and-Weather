using Microsoft.EntityFrameworkCore;
using Currency_Convert.Models;


namespace Currency_Convert.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WorldCity> WorldCities { get; set; }
    public DbSet<WeatherCode> WeatherCodes { get; set; }  
    public DbSet<TimeHour> TimeHours { get; set; }
  }
}
