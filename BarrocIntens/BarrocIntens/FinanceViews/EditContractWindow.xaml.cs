using BarrocIntens.Data;
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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditContractWindow : Window
    {
        public Contract CurrentContract { get; private set; }
        public EditContractWindow(Contract contract)
        {
            this.InitializeComponent();
            using (var db = new AppDbContext())
            {
                this.CurrentContract = db.Contracts.Include(c => c.Products).Include(c => c.Company).FirstOrDefault(c => c.Id == contract.Id);
            }
            productsListView.ItemsSource = this.CurrentContract.ContractProducts;
            ContractTextBox.Text = CurrentContract.Id.ToString();
            CompanyTextBox.Text = CurrentContract.Company.Name.ToString();
            StartDateTextBox.Text = CurrentContract.StartDate.ToString();
            EndDateTextBox.Text = CurrentContract.EndDate.ToString();
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
