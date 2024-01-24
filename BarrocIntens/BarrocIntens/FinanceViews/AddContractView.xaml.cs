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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.FinanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddContractView : Page
    {
        public Company CurrentCompany { get; set; }
        public List<Product> products = new List<Product>();
        public ObservableCollection<ShopCartItem> shopCartCollection = new ObservableCollection<ShopCartItem>();
        public AddContractView()
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
                if (shopCartItem.Product.Id == selectedProduct.Id)
                {
                    shopCartItem.Amount++;
                    return;
                }
            }

            shopCartCollection.Add(
                new ShopCartItem
                {
                    Product = selectedProduct,
                    Amount = 1
                }
            );
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
                CheckIfButtonIsReady();
            }
            else
            {
                CompanySuggestBox.Text = "No selection";
            }
        }

        public void CheckIfButtonIsReady()
        {
            CreateContractButton.IsEnabled = CurrentCompany != null 
                && StartDatePicker.SelectedDate != null 
                && EndDatePicker.SelectedDate != null;
        }

        private void CreateContractButton_Click(object sender, RoutedEventArgs e)
        {
            Contract newContract = new Contract
            {
                CompanyId = CurrentCompany.Id,
                StartDate = DateOnly.Parse(StartDatePicker.SelectedDate.ToString().Split(" ")[0]),
                EndDate = DateOnly.Parse(EndDatePicker.SelectedDate.ToString().Split(" ")[0]),
            };

            using (var db = new AppDbContext())
            {
                db.Contracts.Add(newContract);
                db.SaveChanges();
            }

            using (var db = new AppDbContext())
            {
                Contract addedContract = db.Contracts.FirstOrDefault(c => c.CompanyId == newContract.CompanyId && c.StartDate == newContract.StartDate && c.EndDate == newContract.EndDate);
                
                foreach (var shopCartItem in shopCartCollection)
                {
                    ContractProduct contractProduct = new ContractProduct
                    {
                        ContractId = addedContract.Id,
                        ProductId = shopCartItem.Product.Id,
                        Amount = shopCartItem.Amount,
                    };
                    db.ContractProducts.Add(contractProduct);
                }

                db.SaveChanges();
                Frame.Navigate(typeof(ContractOverviewView));
            }
        }

        private void StartDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            CheckIfButtonIsReady();
        }

        private void EndDatePicker_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            CheckIfButtonIsReady();
        }

        private void CompanySuggestBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CompanySuggestBox.IsSuggestionListOpen = true;
        }

        private void CompanySuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            using var db = new AppDbContext();
            CompanySuggestBox.ItemsSource = db.Companies.Where(c => c.Name.Contains(CompanySuggestBox.Text)).ToList();
        }
    }
}
