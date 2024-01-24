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

namespace BarrocIntens.MaintenanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessageViewMaintenance : Page
    {
        private ObservableCollection<Product> allProducts = new ObservableCollection<Product>();
        private ObservableCollection<MaintenanceAppointment> allMessages = new ObservableCollection<MaintenanceAppointment>();
        public List<Product> allProductsList = new List<Product>();
        public List<MaintenanceAppointment> allMessagesList = new List<MaintenanceAppointment>();
        public MessageViewMaintenance()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();

            allMessagesList = db.MaintenanceAppointments.Where(m => m.Status == 99).
                Include(m => m.MaintenanceAppointmentProducts).
                Include(m => m.Company).
                Include(m => m.Product).ToList();
            allProductsList = db.Products.ToList();

            foreach (var message in allMessagesList)
            {
                allMessages.Add(message);
            }

            messageListView.ItemsSource = allMessagesList;
        }
    }
}
