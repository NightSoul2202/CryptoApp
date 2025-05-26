using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Services.Interfaces
{
    public interface IInitializable
    {
        void InitializeAsync(object parameter = null);
    }
}
