using BarrocIntens.Data;
using BarrocIntens.PurchaseViews;
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
        public static User CurrentUser { get; set; }
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
                case 6:
                    MaintenanceNavView.Visibility = Visibility.Visible;
                    MaintenancePlannerCalendar.Visibility = Visibility.Visible;
                    MalfunctionMessagePage.Visibility = Visibility.Visible;
                    break;
                case 7:
                    PurchaseNavView.Visibility = Visibility.Visible;
                    StockMessageView.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == lastItem || item.Tag == null)
                return;

            var clickedView = item.Tag.ToString();
            if (!NavigateToView(clickedView)) return;
            lastItem = item;
        }

        private bool NavigateToView(string clickedView)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"BarrocIntens.{clickedView}");

            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
                return false;

            var contentFrame = GetCorrectNavBar(CurrentUser.DepartmentId);
            contentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private Frame GetCorrectNavBar (int departmentId)
        {
            switch (departmentId)
            {
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
                case 6:
                    return MaintenanceFrame;
                    break;
                case 7:
                    return PurchaseFrame;
                default: return null;
            }
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            User.LoggedInUser = null;
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
