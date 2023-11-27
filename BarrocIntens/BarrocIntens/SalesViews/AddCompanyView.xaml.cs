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
    public sealed partial class AddCompanyView : Page
    {
        public AddCompanyView()
        {
            this.InitializeComponent();
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            db.Companies.Add(new Company
            {
                Name = nameInput.Text,
                Phone = phoneInput.Text,
                Address = addressInput.Text,
                Zipcode = zipcodeInput.Text,
                City = cityInput.Text,
                CountryCode = countryInput.SelectedValue.ToString(),
            });
            db.SaveChanges();
            nameInput.Text = string.Empty;
            phoneInput.Text = string.Empty;
            addressInput.Text = string.Empty;
            zipcodeInput.Text = string.Empty;
            cityInput.Text = string.Empty;
            countryInput.SelectedItem = -1;
            
        }
    }
}
