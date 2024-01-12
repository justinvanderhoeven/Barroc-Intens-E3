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
using System.Collections.ObjectModel;
using System.Configuration;
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
    public sealed partial class CustomerOverviewView : Page
    {
        public CustomerOverviewView()
        {
            this.InitializeComponent();

            using var db = new AppDbContext();

            var customers = db.Users.Where(u => u.DepartmentId == 1).ToList();
            customerListView.ItemsSource = customers;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            customerListView.ItemsSource = db.Users.Where(u => u.Name.Contains(searchInput) && u.Department.Id == 1);
        }

        private void deleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customerListView.SelectedItem is User selectedCustomer)
            {
                using var db = new AppDbContext();
                var company = db.Companies.FirstOrDefault(c => c.ContactId == selectedCustomer.Id);
                if (company != null)
                {
                    company.ContactId = null;
                }
                db.Users.Remove(selectedCustomer);
                db.SaveChanges();


                var customers = db.Users.Where(u => u.DepartmentId == 1).ToList();
                customerListView.ItemsSource = customers;
            }
        }

        private void customerListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
