using CryptoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Services.Interfaces
{
    public interface IMarketData
    {
        Task<List<Market>> GetMarketDataAsync(string coinName);
    }
}
