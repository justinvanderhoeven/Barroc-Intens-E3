using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BarrocIntens.Data;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.SalesViews
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCompanyWindow : Window
    {
        private Company _selectedCompany;
        private bool _isCustomer = false;
        public EditCompanyWindow(Company selectedCompany)
        {
            this.InitializeComponent();

            _selectedCompany = selectedCompany;
            CompanyTextBox.Text = _selectedCompany.Name;
            AddressTextBox.Text = _selectedCompany.Address;
            ZipcodeTextBox.Text = _selectedCompany.Zipcode;
            CityTextBox.Text = _selectedCompany.City;
            CountryTextBox.Text = _selectedCompany.CountryCode;

            if (_selectedCompany.ContactMail != null)
            {
                _isCustomer = true;
                ContactTextBox.Text = _selectedCompany.ContactName;
                ContactMailTextBox.Text = _selectedCompany.ContactMail;
            }
            else
            {
                ContactTextBox.IsEnabled = false;
                ContactMailTextBox.IsEnabled = false;
            }
        }

        private void updateCompany_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var selectedCompany = db.Companies.Find(_selectedCompany.Id);

            selectedCompany.Name = CompanyTextBox.Text;
            selectedCompany.Address = AddressTextBox.Text;
            selectedCompany.Zipcode = ZipcodeTextBox.Text;
            selectedCompany.City = CityTextBox.Text;
            selectedCompany.CountryCode = CountryTextBox.Text;
            if(_isCustomer)
            {
                selectedCompany.ContactName = CompanyTextBox.Text;
                selectedCompany.ContactMail = ContactMailTextBox.Text;
            }

            db.SaveChanges();
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
