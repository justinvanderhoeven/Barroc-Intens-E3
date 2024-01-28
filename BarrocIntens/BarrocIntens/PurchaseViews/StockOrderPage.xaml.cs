using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.Gaming.Input;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.PurchaseViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockOrderPage : Page
    {
        private int currentOrderId;
        public StockOrderPage()
        {
            this.InitializeComponent();

            LoadProducts();
        }
        private void LoadProducts()
        {
            using (var db = new AppDbContext())
            {
                var productList = db.Products.ToList();

                foreach (var product in productList)
                {
                    if(IsEnabled == product.NeedsAccepting)
                    {
                        product.IsChecked = true;
                    }
                    else
                    {
                        product.IsChecked = false;
                    }
                }

                StockOrderView.ItemsSource = productList;
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            refreshButton.Content = "Refresh";
            LoadProducts();
        }
    }
}
