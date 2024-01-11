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

        private void deleteCompany_Click(object sender, RoutedEventArgs e)
        {
            if (customerListView.SelectedItem is Company selectedCustomer)
            {
                using var db = new AppDbContext();
                db.Companies.Remove(selectedCustomer);
                db.SaveChanges();

                customerListView.ItemsSource = db.Companies.ToList();
            }
        }

        private void customerListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
