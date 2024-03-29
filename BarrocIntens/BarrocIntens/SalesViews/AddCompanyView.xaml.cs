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

        private bool HasAlphanumeric(string input)
        {
            // Check if the input contains at least one alphanumeric character
            return !string.IsNullOrWhiteSpace(input) && input.Any(char.IsLetterOrDigit);
        }

        private bool IsValidText(string input)
        {
            // Check if the input contains only letters
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
        }

        private bool IsValidNameAndAddress(string input)
        {
            // Check if the input contains only letters
            return !string.IsNullOrWhiteSpace(input) && input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || char.IsDigit(c));
        }

        private async void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            if (string.IsNullOrWhiteSpace(nameInput.Text) ||
                !IsValidNameAndAddress(nameInput.Text) ||
                string.IsNullOrWhiteSpace(phoneInput.Text) ||
                !int.TryParse(phoneInput.Text, out _) ||
                string.IsNullOrWhiteSpace(addressInput.Text) ||
                !IsValidNameAndAddress(addressInput.Text) ||
                string.IsNullOrWhiteSpace(zipcodeInput.Text) ||
                !HasAlphanumeric(zipcodeInput.Text) ||
                string.IsNullOrWhiteSpace(cityInput.Text) ||
                !IsValidText(cityInput.Text) ||
                countryInput.SelectedValue == null)
            {
                ContentDialog wrongCredentialsDialog = new ContentDialog
                {
                    Title = "Create fout",
                    Content = "Please check of je alles goed hebt ingevuld",
                    CloseButtonText = "Ok",
                    XamlRoot = this.XamlRoot,
                };

                ContentDialogResult result = await wrongCredentialsDialog.ShowAsync();
                return; 
            }
            else
            {
                // Add a new company to the database
                db.Companies.Add(new Company
                {
                    Name = nameInput.Text,
                    Phone = phoneInput.Text,
                    Address = addressInput.Text,
                    Zipcode = zipcodeInput.Text,
                    City = cityInput.Text,
                    Country = countryInput.SelectedItem.ToString(),
                });

                // Save changes to the database
                db.SaveChanges();

                // Clear input fields
                nameInput.Text = string.Empty;
                phoneInput.Text = string.Empty;
                addressInput.Text = string.Empty;
                zipcodeInput.Text = string.Empty;
                cityInput.Text = string.Empty;
                countryInput.SelectedItem = string.Empty;

                ContentDialog createdCredentialsDialog = new ContentDialog
                {
                    Title = "Bedrijf toegevoegd",
                    Content = "Continue",
                    CloseButtonText = "Ok",
                    XamlRoot = this.XamlRoot,
                };

                ContentDialogResult result = await createdCredentialsDialog.ShowAsync();
            }
        }

        // Navigate back 
        private void navBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CompanyDetailView));
        }
    }
}
