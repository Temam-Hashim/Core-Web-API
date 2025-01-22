using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAPI.Interface;
using WebAPI.Mapper;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class FMPService : IFMPService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public FMPService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<Stock> GetStockBySymbolAsync(string symbol)
        {
            try
            {
                // Use the symbol parameter in the API request
                var apiUrl = $"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}";
                var result = await _httpClient.GetAsync(apiUrl);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);

                    // Ensure there is data in the response
                    if (tasks != null && tasks.Length > 0)
                    {
                        var stock = tasks[0];
                        return stock.ToStockFromFMP();
                    }

                    // Log no data found for the given symbol
                    Console.WriteLine($"No stock data returned for symbol: {symbol}");
                    return null;
                }
                else
                {
                    // Log API response failure
                    Console.WriteLine($"Failed to fetch stock data for symbol: {symbol}, StatusCode: {result.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error fetching stock data for symbol: {symbol}, Exception: {ex}");
            }

            return null;
        }
    }

}