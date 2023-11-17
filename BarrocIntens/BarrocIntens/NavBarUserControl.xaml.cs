// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using BarrocIntens.MaintenanceViews;
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
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    public sealed partial class NavBarUserControl : UserControl
    {

        public NavBarUserControl()
        {
            this.InitializeComponent();
        }
        private void NavigateToHome(object sender = null, RoutedEventArgs e = null)
        {
            ContentFrame.Navigate(typeof(MaintenancePage));
        }

        private void NavigateToMalfunctionMessagePage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(MalfunctionMessagePage));
        }
    }
}
