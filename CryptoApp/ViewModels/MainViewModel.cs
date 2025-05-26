using CommunityToolkit.Mvvm.Input;
using CryptoApp.Models;
using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace CryptoApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ICoinData _coinData;
        private readonly INavigateData _navigationData;
        private ObservableCollection<Coin> _coins;

        public ObservableCollection<Coin> Coins
        { 
            get => _coins;
            set
            {
                _coins = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> NavigateToDetailsCommand { get; }
        public RelayCommand NavigateToSearchCommand { get; }

        public MainViewModel(ICoinData coinData, INavigateData navigationData)
        {
            _coinData = coinData;
            _navigationData = navigationData;
            NavigateToDetailsCommand = new RelayCommand<object>(NavigateToDetails);
            NavigateToSearchCommand = new RelayCommand(() => _navigationData.NavigateTo<SearchViewModel>());
            LoadCoinsAsync();
        }

        private async void LoadCoinsAsync()
        {
            try
            {
                Coins = new ObservableCollection<Coin>(await _coinData.GetTopCoinsAsync(10));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NavigateToDetails(object parameter)
        {
            if (parameter is Coin coin)
            {
                _navigationData.NavigateTo<CoinDetailsViewModel>(coin);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
