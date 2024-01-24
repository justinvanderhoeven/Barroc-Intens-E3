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
using BarrocIntens.PurchaseViews;
using Microsoft.EntityFrameworkCore;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.MaintenanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductStockView : Page
    {
        public ProductStockView()
        {
            this.InitializeComponent();

            using (var db = new AppDbContext())
            {
                ProductSearchingView.ItemsSource = db.Products.ToList();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextBox.Text;

            using var db = new AppDbContext();
            ProductSearchingView.ItemsSource = db.Products.Where(m => m.Name.Contains(searchInput));
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            using (var db = new AppDbContext())
            {
                ProductSearchingView.ItemsSource = db.Products.ToList();
            }
        }

        private void ProductSearchView_ItemClick(object sender, ItemClickEventArgs p)
        {
            var SelectedItem = (Product)p.ClickedItem;
            var window = new ProductUsingView(SelectedItem.Id);
            window.Closed += ProductUsingWindow_Closed;
            window.Activate();
        }

        private void ProductUsingWindow_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            ProductSearchingView.ItemsSource = db.Products.ToList();
        }

        private void Ov_Checked(object sender, RoutedEventArgs e)
        {
            if (StockCheckBox.IsChecked == true)
            {
                using var db = new AppDbContext();

                ProductSearchingView.ItemsSource = db.Products.Where(p => p.Stock > 0);
                OutStockCheckBox.IsEnabled = false;
            }
        }

        private void Nov_Checked(object sender, RoutedEventArgs e)
        {
            if (OutStockCheckBox.IsChecked == true)
            {
                using var db = new AppDbContext();

                ProductSearchingView.ItemsSource = db.Products.Where(p => p.Stock == 0);
                StockCheckBox.IsEnabled = false;
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void OutStockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            StockCheckBox.IsEnabled = true;
            Refresh();
        }

        private void StockCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OutStockCheckBox.IsEnabled = true;
            Refresh();
        }

        public void Refresh()
        {
            using (var db = new AppDbContext())
            {
                ProductSearchingView.ItemsSource = db.Products.ToList();
            }
        }
    }
}
