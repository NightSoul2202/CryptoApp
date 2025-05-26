using CryptoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Services.Interfaces
{
    public interface ICoinData
    {
        Task<List<Coin>> GetTopCoinsAsync(int count);
        Task<Coin> GetCoinAsync(string search);
        Task<List<PriceHistory>> GetPriceHistoryAsync(string id, string interval);
    }
}
