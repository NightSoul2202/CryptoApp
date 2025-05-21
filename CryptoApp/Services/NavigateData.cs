using CryptoApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CryptoApp.Services
{
    public class NavigateData : INavigateData
    {
        private readonly Frame _frame;
        public NavigateData(Frame frame)
        {
            _frame = frame; 
        }
        public void NavigateTo<TViewModel>(object parameter = null)
        {
            string pageTypeName = typeof(TViewModel).Name.Replace("ViewModel", "Page");
            Type pageType = Type.GetType($"CryptoApp.Views.{pageTypeName}");
            if (pageType != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);
                page.DataContext = App.ServiceProvider.GetService<TViewModel>();
                if(page.DataContext is IInitializable initializable)
                {
                    initializable.Initialize(parameter);
                }
                _frame.Navigate(page);
            }
        }
    }
}
