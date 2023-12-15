using BarrocIntens.Data;
using IronPdf;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.SalesViews
{
    public sealed partial class AddOffersView : Page
    {

        public Company CurrentCompany { get; set; }
        public List<Product> products = new List<Product>();
        public ObservableCollection<ShopCartItem> shopCartCollection = new ObservableCollection<ShopCartItem>();
        public AddOffersView()
        {
            this.InitializeComponent();
            ShopCartListView.ItemsSource = shopCartCollection;
            using (var db = new AppDbContext())
            {
                products = db.Products.ToList();
                ProductsCategoryComboBox.ItemsSource = db.ProductsCategories.ToList();
                CompanySuggestBox.ItemsSource = db.Companies.ToList();
            }
        }

        private void ProductsCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Product> filteredProducts = products.Where(p => p.ProductsCategoryId == (ProductsCategoryComboBox.SelectedItem as ProductsCategory).Id).ToList();
            ProductsListView.ItemsSource = filteredProducts;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)((Button)sender).CommandParameter;
            foreach (var shopCartItem in shopCartCollection)
            {
                if(shopCartItem.Product.Id == selectedProduct.Id)
                {
                    shopCartItem.Amount++;
                    return;
                }
            }
            ShopCartItem item = new ShopCartItem
            {
                Product = selectedProduct,
                Amount = 1
            };
            shopCartCollection.Add(item);
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            ShopCartItem selectedProduct = (ShopCartItem)((Button)sender).CommandParameter;
            for (int i = 0; i < shopCartCollection.Count; i++)
            {
                if (shopCartCollection[i].Product.Id == selectedProduct.Product.Id)
                {
                    if (shopCartCollection[i].Amount > 1)
                    {
                        shopCartCollection[i].Amount--;
                        return;
                    }
                    shopCartCollection.RemoveAt(i);
                    return;
                }
            }
        }

        private void CompanySuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Company selectedCompany)
            {
                CompanySuggestBox.Text = selectedCompany.Name;
                CurrentCompany = selectedCompany;
                CreateOfferButton.IsEnabled = true;
            }
            else
            {
                CompanySuggestBox.Text = "No selection";
            }
        }

        private void CreateOfferButton_Click(object sender, RoutedEventArgs e)
        {
            string path = GetProjectDirectory();
            decimal totalPrice = 0;
            foreach (var shopCartItem in shopCartCollection)
            {
                totalPrice += (shopCartItem.Product.Price * shopCartItem.Amount);
            }
            string tableRows = createHtmlTable();
            var pdf = new ChromePdfRenderer();
            PdfDocument doc = pdf.RenderHtmlAsPdf(
                "<div style='width: 100%; height: 100%;'>" +
                    "<div>" +
                        "<h2>OFFERTE</h2>" +
                        "<div style='display: flex; justify-content: space-between;'>" + 
                            "<div style='padding-top: 10px;'>" +
                                "<p style='font-weight:bold; margin: 0;'>Offerte voor: </p>" +
                                "<p style='margin: 0;'>" + CurrentCompany.Name + "</p>" +
                                "<p style='margin: 0;'>" + CurrentCompany.Address + "</p>" +
                                "<p style='margin: 0;'>" + CurrentCompany.Zipcode + " " + CurrentCompany.City + "</p>" +
                            "</div>" +
                            "<div>" +
                                "<p style='margin: 0;'>Barroc Intens</p>" +
                                "<p style='margin: 0;'>Koffieweg 69</p>" +
                                "<p style='margin: 0; margin-bottom: 8px;'>0420SW Breda</p>" +
                                "<p style='margin: 0;'>koffiegenieter@gmail.com</p>" +
                                "<p style='margin: 0;'>06-80679342</p>" +
                                "<p style='margin: 0;'>www.barrocintens.nl" +
                            "</div>" +
                        "</div>" +
                        "<div style='display: flex; justify-content: space-between; margin-top: 30px'>" +
                            "<div style='display: flex; justify-content: space-between;'>" +
                                "<div>" +
                                    "<p style='margin: 0; font-weight:bold;'>Offertedatum:</p>" +
                                    "<p style='margin: 0; font-weight:bold;'>Offertenummer:</p>" +
                                    "<p style='margin: 0; font-weight:bold;'>Leveringsdatum:</p>" +
                                    "<p style='margin: 0; font-weight:bold;'>Geldig t/m:</p>" +
                                "</div>" +
                                "<div style='margin-left: 40px'>" +
                                    "<p style='margin: 0;'>datum</p>" +
                                    "<p style='margin: 0;'>000000</p>" +
                                    "<p style='margin: 0;'>00-00-0000</p>" +
                                    "<p style='margin: 0;'>00-00-0000</p>" +
                                "</div>" +
                            "</div>" +
                        "</div>" +
                        "<table style='width: 100%; margin-top: 50px;'>" +
                            "<tr style='background: lightgray;'>" +
                                "<th style='fontweight: bold; text-align: left;'>Aantal</th>" +
                                "<th style='fontweight: bold; text-align: left;'>Nummer</th>" +
                                "<th style='fontweight: bold; text-align: left;'>Beschrijving</th>" +
                                "<th style='fontweight: bold; text-align: left;'>Prijs</th>" +
                                "<th style='fontweight: bold; text-align: left;'>Subtotaal</th>" +
                            "</tr>" +
                            tableRows +
                        "</table>" +
                        "<div style=' margin-top: 30px; display:flex; justify-content: space-between;'>" +
                            "<p>Totaal:</p>" +
                            "<p>€" + Math.Round(totalPrice, 2) + "</p>" +
                        "</div>" +
                    "</div>" +
                "</div>");
            doc.SaveAs(path + "\\" + CurrentCompany.Name + "Offer.pdf");
        }

        public string createHtmlTable()
        {
            string tableRow = "";
            foreach (var shopCartItem in shopCartCollection)
            {
                tableRow += "<tr style='background: #E8E9EB;'>" +
                                "<td>" + shopCartItem.Amount + "</th>" +
                                "<td>" + shopCartItem.Product.Id + "</th>" +
                                "<td>" + shopCartItem.Product.Name + "</th>" +
                                "<td>€" + Math.Round(shopCartItem.Product.Price, 2) + "</th>" +
                                "<td>€" + Math.Round(shopCartItem.Product.Price * shopCartItem.Amount, 2) + "</th>" +
                            "</tr> ";
            }
            return tableRow;
        }

        static string GetProjectDirectory()
        {
            // Get the base directory of the application
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate up the directory tree until the solution or project file is found
            while (!Directory.GetFiles(baseDirectory, "*.sln").Any() && !Directory.GetFiles(baseDirectory, "*.csproj").Any())
            {
                DirectoryInfo parent = Directory.GetParent(baseDirectory);

                if (parent == null || parent.FullName == baseDirectory)
                {
                    // Unable to find solution or project file, break out of the loop
                    break;
                }

                baseDirectory = parent.FullName;
            }

            return baseDirectory;
        }
    }
}
