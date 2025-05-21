using CryptoApp.Models;
using CryptoApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Services
{
    public class MarketData : IMarketData
    {
        private readonly HttpClient _httpClient;

        public MarketData(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Market>> GetMarketDataAsync(string coinName)
        {
            var url = $"markets?baseId={coinName}";
            var markets = await GetProcess<List<Market>>(url);
            return markets;
        }

        private async Task<TType> GetProcess<TType>(string url) where TType : class, new()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                var content = await responseMessage.Content.ReadAsStringAsync();
                var returnObject = JsonConvert.DeserializeObject<CoinResponse<TType>>(content);
                return returnObject?.Data ?? new TType();
            }
            catch (Exception ex)
            {
                return new TType();
            }
        }
    }
}
