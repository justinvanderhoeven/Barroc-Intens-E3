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

namespace BarrocIntens.SalesViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompanyDetailView : Page
    {
        public CompanyDetailView()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            var companies = db.Companies
               .ToList();
            companyListView.ItemsSource = companies;
        }

        private void deleteCompany_Click(object sender, RoutedEventArgs e)
        {
            if (companyListView.SelectedItem is Company selectedCompany)
            {
                using var db = new AppDbContext();
                db.Companies.Remove(selectedCompany);
                db.SaveChanges();

                companyListView.ItemsSource = db.Companies.ToList();
            }
        }

        private void addCompany_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddCompanyView));
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            companyListView.ItemsSource = db.Companies.Where(m => m.Name.Contains(searchInput));
        }

        private void companyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedCompany = (Company)e.ClickedItem;
            Frame.Navigate(typeof(EditCompanyView), selectedCompany);
        }
    }
}
