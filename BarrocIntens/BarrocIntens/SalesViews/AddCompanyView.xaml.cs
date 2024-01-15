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
    public sealed partial class AddCompanyView : Page
    {
        public AddCompanyView()
        {
            this.InitializeComponent();
        }

        private async void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            if (string.IsNullOrWhiteSpace(nameInput.Text) ||
                string.IsNullOrWhiteSpace(phoneInput.Text) ||
                string.IsNullOrWhiteSpace(addressInput.Text) ||
                string.IsNullOrWhiteSpace(zipcodeInput.Text) ||
                string.IsNullOrWhiteSpace(cityInput.Text) ||
                countryInput.SelectedValue == null)
            {
                ContentDialog wrongCredentialsDialog = new ContentDialog
                {
                    Title = "Create Failed",
                    Content = "Please check if you filled in all fields",
                    CloseButtonText = "Ok",
                    XamlRoot = this.XamlRoot,
                };

                ContentDialogResult result = await wrongCredentialsDialog.ShowAsync();
                return; 
            }

            // Add a new company to the database
            db.Companies.Add(new Company
            {
                Name = nameInput.Text,
                Phone = phoneInput.Text,
                Address = addressInput.Text,
                Zipcode = zipcodeInput.Text,
                City = cityInput.Text,
                CountryCode = countryInput.SelectedValue.ToString(),
            });

            // Save changes to the database
            db.SaveChanges();

            // Clear input fields
            nameInput.Text = string.Empty;
            phoneInput.Text = string.Empty;
            addressInput.Text = string.Empty;
            zipcodeInput.Text = string.Empty;
            cityInput.Text = string.Empty;
            countryInput.SelectedItem = -1;
        }

        // Navigate back 
        private void navBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CompanyDetailView));
        }
    }
}
