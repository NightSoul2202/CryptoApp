using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigateData _navigateData;

        public MainWindowViewModel(INavigateData navigateData)
        {
            _navigateData = navigateData;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
