using CommunityToolkit.Mvvm.Input;
using CryptoApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CryptoApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigateData _navigateData;
        private bool _isMenuOpen;
        private string _selectedMenuItem;

        public bool IsMenuOpen { 
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged();
            }
        }

        public string SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                _selectedMenuItem = value;
                OnPropertyChanged();
                if (value != null)
                {
                    NavigateToPage(value);
                    IsMenuOpen = false;
                }
            }
        }

        public RelayCommand ToggleMenuCommand { get; }
        public RelayCommand NavigateToHomeCommand { get; }

        public MainWindowViewModel(INavigateData navigateData)
        {
            _navigateData = navigateData;
            ToggleMenuCommand = new RelayCommand(() => IsMenuOpen = !IsMenuOpen);
            NavigateToHomeCommand = new RelayCommand(() => _navigateData.NavigateTo<MainViewModel>());
        }

        private void NavigateToPage(string pageTag)
        {
            switch (pageTag)
            {
                case "System.Windows.Controls.ListBoxItem: Main Page":
                    _navigateData.NavigateTo<MainViewModel>();
                    break;
                case "System.Windows.Controls.ListBoxItem: Search Page":
                    _navigateData.NavigateTo<SearchViewModel>();
                    break;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
