using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class PriceHistory
    {
        public decimal PriceUsd { get; set; }
        public long Time { get; set; }
        public string Date { get; set; }
        public decimal CirculatingSupply { get; set; }
    }
}
