// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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

namespace BarrocIntens.MaintenanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MalfunctionMessagePage : Page
    {
        public MalfunctionMessagePage()
        {
            this.InitializeComponent();
            using (var db = new AppDbContext())
            {
                var malfunctions = db.MaintenanceAppointments.Include(m => m.Company)
                .OrderBy(d => d.DateAdded).ToList();
                MalfunctionListView.ItemsSource = malfunctions;
            }
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Herlaad";
            using (var db = new AppDbContext())
            {
                // This code will be executed when the button is clicked
                var malfunctions = db.MaintenanceAppointments.Include(m => m.Company)
                //.Where(m => m.Id.Contains(SearchTextBox.Text))
                .OrderBy(d => d.DateAdded).ToList();
                MalfunctionListView.ItemsSource = malfunctions;
            }
        }
    }
}
