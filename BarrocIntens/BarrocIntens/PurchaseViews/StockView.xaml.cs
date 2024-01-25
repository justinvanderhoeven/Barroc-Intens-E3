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
using System.Diagnostics;
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
    public sealed partial class StockView : Page
    {
        public StockView()
        {
            this.InitializeComponent();

            using var db = new AppDbContext();
            productCategoryInput.ItemsSource = db.ProductsCategories.ToList();
            productCategoryInput.DisplayMemberPath = "Name";

        }
  
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            var selectedCategory = (ProductsCategory)productCategoryInput.SelectedItem;

            Product product;
            int stock = int.Parse(stockInput.Text);
            if (stock >= 5000 && IsUserInRole("HeadPurchase"))
            {
                product = new Product
                {
                    Name = nameInput.Text,
                    Description = descriptionInput.Text,
                    Price = decimal.Parse(priceInput.Text),
                    Stock = int.Parse(stockInput.Text),
                    ProductsCategoryId = selectedCategory.Id,
                };
            } else if (stock >= 5000 && !IsUserInRole("HeadPurchase"))
            {
                product = new Product
                {
                    Name = nameInput.Text,
                    Description = descriptionInput.Text,
                    Price = decimal.Parse(priceInput.Text),
                    Stock = 0,
                    NeedsAccepting = true,
                    StockToChangeTo = stock,
                    ProductsCategoryId = selectedCategory.Id,
                };
            } else
            {
                product = new Product
                {
                    Name = nameInput.Text,
                    Description = descriptionInput.Text,
                    Price = decimal.Parse(priceInput.Text),
                    Stock = int.Parse(stockInput.Text),
                    ProductsCategoryId = selectedCategory.Id,
                };
            }


                // Save to the database
                db.Products.Add(product);
                
                db.SaveChanges();
           
            // Clear input fields after processing
            nameInput.Text = string.Empty;
            descriptionInput.Text = string.Empty;
            priceInput.Text = string.Empty;
            productCategoryInput.SelectedItem = -1;
        }

        private bool IsUserInRole(string roleName)
        {
            return User.LoggedInUser.DepartmentId == 7;
        }
   
        private void BackToStockButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StockSearchView));
        }
    }
}
