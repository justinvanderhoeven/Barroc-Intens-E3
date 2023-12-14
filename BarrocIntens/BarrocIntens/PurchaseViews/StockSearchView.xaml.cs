using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.PurchaseViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockSearchView : Page
    {
        public StockSearchView()
        {
            this.InitializeComponent();

            using (var db = new AppDbContext())
            {
                StockSearchingView.ItemsSource = db.Products.ToList();
            }
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs m)
        {
            var searchInput = searchTextBox.Text;

            using var db = new AppDbContext();
            StockSearchingView.ItemsSource = db.Products.Where(m => m.Name.Contains(searchInput));
        }

        private void CreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new StockView();
            window.Activate();
            window.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            using (var db = new AppDbContext())
            {
                StockSearchingView.ItemsSource = db.Products.ToList();
            }
        }
    }
}
