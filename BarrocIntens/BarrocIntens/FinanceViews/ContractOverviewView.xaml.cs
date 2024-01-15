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
using Microsoft.EntityFrameworkCore;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.FinanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContractOverviewView : Page
    {

        public ContractOverviewView()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            var contracts = db.Contracts.Include(c => c.Company).Include(c => c.ContractProducts)
               .ToList();
            contractListView.ItemsSource = contracts;
        }

        private void deleteCompany_Click(object sender, RoutedEventArgs e)
        {
            if (contractListView.SelectedItem is Contract selectedContract)
            {
                using var db = new AppDbContext();
                db.Contracts.Remove(selectedContract);
                db.SaveChanges();

                contractListView.ItemsSource = db.Contracts.Include(c => c.ContractProducts).Include(c => c.Company)
               .ToList();
            }
        }

        private void addCompany_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddContractView));
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = searchTextbox.Text;

            using var db = new AppDbContext();
            contractListView.ItemsSource = db.Companies.Where(m => m.Name.Contains(searchInput));
        }

        private void contractListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Contract selectedContract)
            {
                Window window = new EditContractWindow(selectedContract);
                window.Activate();
            }
        }
    }
}
