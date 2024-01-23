// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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

namespace BarrocIntens.MaintenanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MaintenanceCalendarView : Page
    {
        public MaintenanceCalendarView()
        {
            this.InitializeComponent();
            using (var db = new AppDbContext())
            {
                var currentUser = MainPage.CurrentUser;

                var malfunctions = db.MaintenanceAppointments.Include(m => m.Company)
                .Where(c => c.UserId == currentUser.Id)
                .OrderBy(d => d.DateAdded).ToList();
                MalfunctionListView.ItemsSource = malfunctions;
            }
        }

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            using var db = new AppDbContext();
            var currentUser = MainPage.CurrentUser;
            var calendarItemDate = args.Item.Date;
            var relevantMaintenanceAppointments = db.MaintenanceAppointments
                .Where(item => item.DateAdded.Date == calendarItemDate.Date && item.UserId == currentUser.Id ).ToList();

            args.Item.DataContext = relevantMaintenanceAppointments;

            if (relevantMaintenanceAppointments.Count() == 0)
            {
                args.Item.IsBlackout = true;
            }
        }

        private async void DayItemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedCalendarItem = (MaintenanceAppointment)e.ClickedItem;

            var dialog = new ContentDialog()
            {
                Title = clickedCalendarItem.Id,
                Content = $"Description: {clickedCalendarItem.Description}\nCreated at: {clickedCalendarItem.DateAdded}",
                CloseButtonText = "Close",
                XamlRoot = this.XamlRoot,
            };

            await dialog.ShowAsync();
        }

        private void workOrder_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is MaintenanceAppointment clickedProduct)
            {
                var workOrder = new WorkOrderView(clickedProduct);
                workOrder.Activate();
            }
        }
    }
}
