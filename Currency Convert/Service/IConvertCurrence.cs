using static Currency_Convert.Service.ConvertCurrence;

namespace Currency_Convert.Service
{
  public interface IConvertCurrence
  {
    Task<decimal> GetConversionRateAsync(string fromCurrency, string toCurrency, decimal amount);
  }
}
