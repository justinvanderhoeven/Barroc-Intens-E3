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
using Windows.Gaming.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.PurchaseViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StockEditView : Window
    {
        private int currentProductId;
        public StockEditView(int productId)
        {
            this.InitializeComponent();

            this.currentProductId = productId;

            using var db = new AppDbContext();

            var product = db.Products
                .First(p => p.Id == productId);
            nameInput.Text = product.Name;
            descriptionInput.Text = product.Description;
            priceInput.Text = product.Price.ToString();
            stockInput.Text = product.Stock.ToString();
            productCategoryInput.ItemsSource = db.ProductsCategories;
            productCategoryInput.SelectedItem = product.ProductsCategory;
        }
        private void EditProductButton_Click(object sender, RoutedEventArgs p)
        {
            using var db = new AppDbContext();
            var product = db.Products
                .First(p => p.Id == currentProductId);
            product.Name = nameInput.Text;
            product.Description = descriptionInput.Text;
            product.Price = decimal.Parse(priceInput.Text);
            product.Stock = int.Parse(stockInput.Text);
            product.ProductsCategory = (ProductsCategory)productCategoryInput.SelectedItem;

            db.SaveChanges();
            //nameInput.Text = string.Empty;
            //descriptionInput.Text = string.Empty;
            //priceInput.Text = "0";
            //stockInput.Text = "0";
            this.Close();
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs p)
        {
            using var db = new AppDbContext();
            {
                var product = db.Products.First(p => p.Id == currentProductId);
                db.Products.Remove(product);
                db.SaveChanges();
                this.Close();
            }
        }

        private void BackToStockButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
