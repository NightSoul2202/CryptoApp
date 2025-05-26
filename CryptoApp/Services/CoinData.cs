using CryptoApp.Models;
using CryptoApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Services
{
    public class CoinData : ICoinData
    {
        private readonly HttpClient _httpClient;

        public CoinData(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<Coin> GetCoinAsync(string search)
        {
            var url = $"assets?search={search}&limit=1";
            var coins = await GetProcess<List<Coin>>(url);

            return coins[0];
        }

        public async Task<List<Coin>> GetTopCoinsAsync(int count)
        {
            var url = $"assets?limit={count}";
            var coins = await GetProcess<List<Coin>>(url);

            return coins;
        }

        private async Task<TType> GetProcess<TType>(string url) where TType : class, new()
        {
            var responseMessage = await _httpClient.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            var content = await responseMessage.Content.ReadAsStringAsync();
            var returnObject = JsonConvert.DeserializeObject<CoinResponse<TType>>(content);

            return returnObject?.Data ?? new TType();
        }
    }
}
