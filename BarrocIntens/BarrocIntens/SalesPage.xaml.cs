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
    public sealed partial class SalesPage : Page
    {
        private NavigationViewItem lastItem;
        public SalesPage()
        {
            this.InitializeComponent();
        }

        private void SalesNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
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
            var view = Assembly.GetExecutingAssembly().GetType($"BarrocIntens.SalesViews.{clickedView}");

            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
                return false;

            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }
    }
}
