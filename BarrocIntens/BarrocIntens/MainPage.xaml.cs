using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public User CurrentUser { get; set; }
        private NavigationViewItem lastItem;
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentUser = e.Parameter as User;

            switch (CurrentUser.DepartmentId)
            {
                case 1:
                    UserNavView.Visibility = Visibility.Visible;
                    break;
                case 2:
                    FinanceNavView.Visibility = Visibility.Visible;
                    break;
                case 3:
                    MaintenanceNavView.Visibility = Visibility.Visible;
                    break;
                case 4:
                    SalesNavView.Visibility = Visibility.Visible;
                    break;
                case 5:
                    PurchaseNavView.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == lastItem)
                return;

            var clickedView = item.Tag.ToString();
            if (!NavigateToView(clickedView)) return;
            lastItem = item;
        }

        private bool NavigateToView(string clickedView)
        {
            string viewFolder = GetCorrectViews(CurrentUser.DepartmentId);
            var view = Assembly.GetExecutingAssembly().GetType($"BarrocIntens.{viewFolder}.{clickedView}");

            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
                return false;

            var contentFrame = GetCorrectNavBar(CurrentUser.DepartmentId);
            contentFrame.Navigate(view, CurrentUser, new EntranceNavigationTransitionInfo());
            return true;
        }

        private Frame GetCorrectNavBar (int departmentId)
        {
            switch (departmentId)
            {
                case 1:
                    return UserFrame;
                    break;
                case 2:
                    return FinanceFrame;
                    break;
                case 3:
                    return MaintenanceFrame;
                    break;
                case 4:
                    return SalesFrame;
                    break;
                case 5:
                    return PurchaseFrame;
                    break;
                default: return null;
            }
        }

        private string GetCorrectViews (int departmentId)
        {
            switch (departmentId)
            {
                case 1:
                    return "UserViews";
                    break;
                case 2:
                    return "FinanceViews";
                    break;
                case 3:
                    return "MaintenanceViews";
                    break;
                case 4:
                    return "SalesViews";
                    break;
                case 5:
                    return "PurchaseViews";
                    break;

            }
            return "Error";
        }
    }
}
