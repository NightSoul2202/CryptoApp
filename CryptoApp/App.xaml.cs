using CryptoApp.Services;
using CryptoApp.Services.Interfaces;
using CryptoApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace CryptoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<HttpClient>(provider =>
            {
                var apiKey = configuration["CoinCapApi:ApiKey"];

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://rest.coincap.io/v3/");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer: {apiKey}");

                return client;
            });

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ICoinData, CoinData>();
            services.AddSingleton<IMarketData, MarketData>();
            services.AddSingleton<INavigateData, NavigateData>(provider => new NavigateData(((MainWindow)Current.MainWindow).MainFrame));
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<CoinDetailsViewModel>();
            services.AddSingleton<SearchViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow();
            var navigateData = ServiceProvider.GetRequiredService<INavigateData>();
            mainWindow.Show();

            navigateData.NavigateTo<MainViewModel>();
        }
    }

}
