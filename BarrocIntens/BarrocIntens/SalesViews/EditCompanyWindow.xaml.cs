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

            nameBox.Text = _selectedCompany.Name;
            cityBox.Text = _selectedCompany.City.ToString();
            zipcodeBox.Text = _selectedCompany.Zipcode.ToString();
            bkrBox.Text = _selectedCompany.BkrCheckedAt.ToString();
            phoneBox.Text = _selectedCompany.Phone.ToString();

            if (_selectedCompany.ContactMail != null)
            {
                _isCustomer = true;
                contactNameBox.Text = _selectedCompany.ContactName;
                contactMailBox.Text = _selectedCompany.ContactMail;
            }
        }

        private void updateCompany_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var selectedCompany = db.Companies.Find(_selectedCompany.Id);

            selectedCompany.Name = nameBox.Text;
            selectedCompany.City = cityBox.Text;
            selectedCompany.Zipcode = zipcodeBox.Text;
            selectedCompany.BkrCheckedAt = DateTime.Parse(bkrBox.Text);
            selectedCompany.Phone = (phoneBox.Text);

            db.SaveChanges();
        }
    }
}
