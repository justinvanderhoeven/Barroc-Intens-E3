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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.PurchaseViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockMessagePage : Page
    {
        public StockMessagePage()
        {
            this.InitializeComponent();

            using (var db = new AppDbContext())
            {
                StockMessageView.ItemsSource = db.Products.Where(p => p.Stock == 0);
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            refreshButton.Content = "Refresh";
            using (var db = new AppDbContext())
            {
                StockMessageView.ItemsSource = db.Products.Where(p => p.Stock == 0);
            }
        }
    }
}
