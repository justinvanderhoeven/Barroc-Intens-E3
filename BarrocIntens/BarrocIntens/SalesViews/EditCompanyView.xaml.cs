using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using BarrocIntens.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.SalesViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditCompanyView : Page
    {
        private Company _selectedCompany;
        public EditCompanyView(Company selectedCompany)
        {
            this.InitializeComponent();

            using var db = new AppDbContext();
            db.Companies.Attach(selectedCompany);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _selectedCompany = e.Parameter as Company;

            nameBox.Text = _selectedCompany.Name;
            cityBox.Text = _selectedCompany.City.ToString();
            zipcodeBox.Text = _selectedCompany.Zipcode.ToString();
            contactBox.Text = _selectedCompany.Contact.ToString();
            bkrBox.Text = _selectedCompany.BkrCheckedAt.ToString();
            phoneBox.Text = _selectedCompany.Phone.ToString();

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

            Frame.Navigate(typeof(CompanyDetailView));
        }
    }
}
