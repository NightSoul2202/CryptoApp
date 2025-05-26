using CryptoApp.Models;
using CryptoApp.Services.Interfaces;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoApp.ViewModels
{
    public class CoinDetailsViewModel : INotifyPropertyChanged, IInitializable
    {
        private readonly ICoinData _coinData;
        private readonly IMarketData _marketData;
        private Coin _coin;
        private ObservableCollection<Market> _markets;
        private ObservableCollection<PriceHistory> _priceHistory;
        private ISeries[] _series;

        public Coin Coin
        {
            get => _coin;
            set
            {
                _coin = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Market> Markets
        {
            get => _markets;
            set
            {
                _markets = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<PriceHistory> PriceHistory
        {
            get => _priceHistory;
            set
            {
                _priceHistory = value;
                OnPropertyChanged();
            }
        }
        public ISeries[] Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged();
            }
        }
        public CoinDetailsViewModel(ICoinData coinData, IMarketData marketData)
        {
            _coinData = coinData;
            _marketData = marketData;
            Series = Array.Empty<ISeries>();
        }
        public async void InitializeAsync(object parameter = null)
        {
            try
            {
                if (parameter is Coin coin)
                {
                    Coin = await _coinData.GetCoinAsync(coin.Id);
                    Markets = new ObservableCollection<Market>(await _marketData.GetMarketDataAsync(coin.Id));

                    var priceHistory = await _coinData.GetPriceHistoryAsync(coin.Id, "d1");
                    PriceHistory = new ObservableCollection<PriceHistory>(priceHistory);
                    UpdateSeries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateSeries()
        {
            if (PriceHistory == null || !PriceHistory.Any())
            {
                Series = Array.Empty<ISeries>();
                return;
            }

            Series =
            [
                new ColumnSeries<DateTimePoint>
                {
                   Values = PriceHistory.Select(c => new DateTimePoint
                   {
                       DateTime = DateTimeOffset.FromUnixTimeMilliseconds(c.Time).DateTime,
                       Value = (double)c.PriceUsd
                   }).ToArray(),
                   Name = "Price (USD)"
                }
            ];
        }

        public ICartesianAxis[] XAxes { get; set; } = [
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MMMM dd"))
        ];

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
