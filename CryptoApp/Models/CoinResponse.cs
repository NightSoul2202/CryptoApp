using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class CoinResponse
    {
        public T Data { get; set; }
        public long Timestamp { get; set; }
    }
}
