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
        }

        private void deleteCompany_Click(object sender, RoutedEventArgs e)
        {

        }

        private void companyListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            customerListView.ItemsSource = db.Companies.Where(m => m.Name.Contains(searchInput));
        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
