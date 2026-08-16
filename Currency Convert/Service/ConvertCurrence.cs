using Microsoft.Extensions.Primitives;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Currency_Convert.Service
{
  public class ConvertCurrence : IConvertCurrence
  {
    private  HttpClient _httpClient;
    private string _apiUrl = "https://api.frankfurter.dev/v2/rate";

    public ConvertCurrence(HttpClient httpClient)
    {
      _httpClient = httpClient;
    } 

    public async Task<decimal> GetConversionRateAsync(string fromCurrency, string toCurrency, decimal amount)
    {
      decimal rate;


      try
      {
        var response = await _httpClient.GetFromJsonAsync<RateResponse>($"https://api.frankfurter.dev/v2/rate/{fromCurrency}/{toCurrency}");
        //response.EnsureSuccessStatusCode();
        //var content = await response.Content.ReadAsStringAsync();
        if (response != null)
        {
          return response.Rate * amount;
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

      return 0;
    }

    public class RateResponse
    {
      [JsonPropertyName("date")]
      public string Date { get; set; }
      [JsonPropertyName("base")]
      public string Base { get; set; }
      [JsonPropertyName("quote")]
      public string Quote { get; set; }
      [JsonPropertyName("rate")]
      public decimal Rate { get; set; }
    }


  }
}

