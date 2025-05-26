using CommunityToolkit.Mvvm.Input;
using CryptoApp.Models;
using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace CryptoApp.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly ICoinData _coinData;
        private readonly INavigateData _navigationData;
        private ObservableCollection<Coin> _searchResults;
        private string _searchQuery;

        public ObservableCollection<Coin> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand<object> NavigateToDetailsCommand { get; }

        public SearchViewModel(ICoinData coinData, INavigateData navigateData)
        {
            _coinData = coinData;
            _navigationData = navigateData;
            SearchCommand = new RelayCommand(() => SearchAsync());
            NavigateToDetailsCommand = new RelayCommand<object>(NavigateToDetails);
            SearchResults = new ObservableCollection<Coin>();
        }

        private void NavigateToDetails(object parameter)
        {
            if (parameter is Coin coin)
            {
                _navigationData.NavigateTo<CoinDetailsViewModel>(coin);
            }
        }

        private async void SearchAsync()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                try
                {
                    var coin = await _coinData.GetCoinAsync(SearchQuery.ToLower());
                    SearchResults = new ObservableCollection<Coin> { coin };
                }
                catch (Exception ex)
                {
                    SearchResults.Clear();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                SearchResults.Clear();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
