using BarrocIntens.Data;
using BarrocIntens.SalesViews;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.MaintenanceViews
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkOrderView : Window
    {
        private MaintenanceAppointment _currentAppointment;
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        private ObservableCollection<Product> allProducts = new ObservableCollection<Product>();
        public List<Product> allProductsList = new List<Product>();
        public ObservableCollection<Product> addedProducts = new ObservableCollection<Product>();
        public WorkOrderView(MaintenanceAppointment currentAppointment)
        {
            this.InitializeComponent();

            _currentAppointment = currentAppointment;

            using var db = new AppDbContext();
            db.MaintenanceAppointments
                .Attach(currentAppointment);

            allProductsList = db.Products.ToList();
            
            foreach (var product in allProductsList)
            {
                if (product.ProductsCategoryId == 3)
                {
                    allProducts.Add(product);
                }
            }
            productPartsToAddListView.ItemsSource = allProducts;
            addedProductPartsListView.ItemsSource = addedProducts;

            var company = db.Companies.FirstOrDefault(c => c.Id == currentAppointment.CompanyId);

            nameCompanyInput.Text = company.Name;
            nameMechanicInput.Text = User.LoggedInUser.Name;
            Description.Text = currentAppointment.Description;
        }
        private void addProductPartButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((Button)sender).Tag as Product;
            Product productToRemove = products.FirstOrDefault(p => p.Id == product.Id);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
            }
            addedProducts.Add(product);
        }
        private void removeProductPartButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((Button)sender).Tag as Product;
            Product productToRemove = addedProducts.FirstOrDefault(p => p.Id == product.Id);
            if (productToRemove != null)
            {
                addedProducts.Remove(productToRemove);
            }
            products.Add(product);
        }
        private void AddWorkOrderButton_Click(object sender, RoutedEventArgs e )
        {
            using var db = new AppDbContext();           
            var currentAppointment = db.MaintenanceAppointments.Find(_currentAppointment.Id);
            //currentAppointment.MaintenanceAppointmentProducts = addedProducts;
            currentAppointment.Status = 99;
            currentAppointment.EndTime = DateTime.Now;

            foreach ( var product in currentAppointment.MaintenanceAppointmentProducts )
            {
                product.MaintenanceAppointmentId = currentAppointment.Id;
            }
            db.SaveChanges();
            this.Close();
        }
    }
}
