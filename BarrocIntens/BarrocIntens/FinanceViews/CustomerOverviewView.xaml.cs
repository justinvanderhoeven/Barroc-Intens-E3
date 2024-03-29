using BarrocIntens.Data;
using BarrocIntens.MaintenanceViews;
using BarrocIntens.SalesViews;
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

            customerListView.ItemsSource = db.Companies.Where(c => c.ContactMail != null).ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            customerListView.ItemsSource = db.Companies.Where(c => c.Name.Contains(searchInput) && c.ContactMail != null);
        }

        private void deleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customerListView.SelectedItem is Company selectedCustomer)
            {
                using var db = new AppDbContext();
                var company = db.Companies.FirstOrDefault(c => c.Id == selectedCustomer.Id);
                if (company != null)
                {
                    company.ContactMail = null;
                }
                db.SaveChanges();


                var customers = db.Companies.Where(c => c.ContactMail != null).ToList();
                customerListView.ItemsSource = customers;
            }
        }

        private void CreateMalfunctionButton_Click(object sender, RoutedEventArgs e)
        {
            Company company = ((Button)sender).Tag as Company;
            Window window = new CreateMalfunctionMessageWindow(company);
            window.Activate();
        }

        private void customerListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Company selectedCompany)
            {
                Window window = new EditCompanyWindow(selectedCompany);
                window.Closed += EditCompanyWindow_Closed; ;
                window.Activate();
            }
        }

        private void EditCompanyWindow_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            customerListView.ItemsSource = db.Companies.Where(c => c.ContactMail != null).ToList();
        }
    }
}
