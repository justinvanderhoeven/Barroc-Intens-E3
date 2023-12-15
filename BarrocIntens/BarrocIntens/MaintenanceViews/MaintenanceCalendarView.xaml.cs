// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using BarrocIntens.Data;
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
    public sealed partial class MaintenanceCalendarView : Page
    {
        public MaintenanceCalendarView()
        {
            this.InitializeComponent();
            using (var db = new AppDbContext())
            {
                var currentUser = MainPage.CurrentUser;

                var malfunctions = db.MaintenanceAppointments
                .Where(c => c.UserId.Equals(currentUser.Id))
                .OrderBy(d => d.DateAdded).ToList();
            }
        }

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            using var db = new AppDbContext();
            var calendarItemDate = args.Item.Date;
            var relevantMaintenanceAppointments = db.MaintenanceAppointments.Where(item => item.StartTime.Date == calendarItemDate.Date).ToList();

            // De DataContext is vanuit de xaml te benaderen met {Binding}
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
                Content = $"Start: {clickedCalendarItem.StartTime}\nEnd: {clickedCalendarItem.EndTime}\nDescription: {clickedCalendarItem.Description}",
                CloseButtonText = "Close",
                XamlRoot = this.XamlRoot,
            };

            await dialog.ShowAsync();
        }
    }
}
