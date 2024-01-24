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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.UserViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeaseContractView : Page
    {
        public Company CurrentCompany { get; set; }
        public ObservableCollection<Contract> Contracts { get; } = new ObservableCollection<Contract>();

        public LeaseContractView(Company currentCompany)
        {
            this.InitializeComponent();

            CurrentCompany = currentCompany;

            using (var db = new AppDbContext())
            {
                // Assuming there's a navigation property in the Company class like Contracts
                CurrentCompany = db.Companies.Include(c => c.CompanyContracts).FirstOrDefault(c => c.Id == CurrentCompany.Id);

                if (CurrentCompany != null)
                {
                    Contracts.Clear(); // Clear existing contracts

                    foreach (var contract in CurrentCompany.CompanyContracts)
                    {
                        Contracts.Add(contract);
                    }

                    // Check if controls are not null before setting ItemsSource
                    if (companyIdListView != null)
                        companyIdListView.ItemsSource = Contracts;

                    if (dateListView != null)
                        dateListView.ItemsSource = Contracts;

                    if (productsListView != null)
                        productsListView.ItemsSource = Contracts;
                }
            }
        }

    }
}

