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

        }
        private void removeProductPartButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddWorkOrderButton_Click(object sender, RoutedEventArgs e )
        {
            //using var db = new AppDbContext();

            //var currentAppointment = db.MaintenanceAppointments.Find(_currentAppointment.Id);

            //var selectedProductPart = (Product)productPartComboBox.SelectedItem;

            //if( selectedProductPart != null )
            //{
            //    //selectedProductPart.MaintenanceAppointments = selectedProductPart.Name;  
            //}
            //else
            //{
            //    // error message
            //}

            //db.SaveChanges();
            //this.Close();
        }
        //public void convertProductsList()
        //{
        //    foreach (var product in allProducts)
        //    {
        //        allProducts.Add(product);
        //    }
        //}
    }
}
