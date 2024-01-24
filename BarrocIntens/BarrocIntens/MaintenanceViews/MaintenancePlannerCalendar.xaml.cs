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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

namespace BarrocIntens.MaintenanceViews
{
    public sealed partial class MaintenancePlannerCalendar : Page
    {
        private int selectedUserId = -1;

        public MaintenancePlannerCalendar()
        {
            this.InitializeComponent();

            using (var db = new AppDbContext())
            {
                var currentUser = MainPage.CurrentUser;

                var malfunctionslist = db.MaintenanceAppointments.Include(m => m.Company)
                    .Where(c => c.UserId != currentUser.Id)
                    .OrderBy(d => d.DateAdded).ToList();
                MalfunctionListView.ItemsSource = malfunctionslist;

                UserSuggestBox.ItemsSource = db.Users.Where(u => u.DepartmentId == 3).ToList();
            }
        }

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            using var db = new AppDbContext();
            var currentUser = MainPage.CurrentUser;
            var calendarItemDate = args.Item.Date;
            var relevantMaintenanceAppointments = db.MaintenanceAppointments
                .Where(item => item.DateAdded.Date == calendarItemDate.Date && item.UserId != currentUser.Id).ToList();

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
                Content = $"Beschrijving: {clickedCalendarItem.Description}\nAangemaakt op: {clickedCalendarItem.DateAdded}",
                CloseButtonText = "Sluit",
                XamlRoot = this.XamlRoot,
            };

            await dialog.ShowAsync();
        }

        private void UserSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Data.User selectedUser)
            {
                selectedUserId = selectedUser.Id;

                UserSuggestBox.Text = selectedUser.Name;
            }
            else
            {
                UserSuggestBox.Text = "";
            }
        }

        private async void BindUser_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceAppointment selectedAppointment = (MaintenanceAppointment)((Button)sender).CommandParameter;


            if (UserSuggestBox.Text != string.Empty && selectedUserId != -1)
            {
                using (var db = new AppDbContext())
                {
                    // Find the appointment in the database by its AppointmentId
                    var appointmentToUpdate = db.MaintenanceAppointments
                        .SingleOrDefault(a => a.Id == selectedAppointment.Id);

                    if (appointmentToUpdate != null)
                    {
                        // Update the UserId
                        appointmentToUpdate.UserId = selectedUserId;

                        // Save changes to the database
                        db.SaveChanges();

                        var currentUser = MainPage.CurrentUser;

                        var malfunctionslist = db.MaintenanceAppointments.Include(m => m.Company)
                            .Where(c => c.UserId != currentUser.Id && c.UserId == null)
                            .OrderBy(d => d.DateAdded).ToList();
                        MalfunctionListView.ItemsSource = malfunctionslist;

                        ContentDialog bindCredentialsDialog = new ContentDialog
                        {
                            Title = "Gebruiker gekoppeld",
                            CloseButtonText = "Ok",
                            XamlRoot = this.XamlRoot,
                        };

                        ContentDialogResult result = await bindCredentialsDialog.ShowAsync();

                    }
                }
            }
            else
            {
                ContentDialog wrongCredentialsDialog = new ContentDialog
                {
                    Title = "Geen gebruiker",
                    Content = "check of je een gebruiker hebt gekozen",
                    CloseButtonText = "Ok",
                    XamlRoot = this.XamlRoot,
                };

                ContentDialogResult result = await wrongCredentialsDialog.ShowAsync();
            }
        }

        private void UserSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            using var db = new AppDbContext();
            UserSuggestBox.ItemsSource = db.Users.Where(u => u.DepartmentId == 3 && u.Name.Contains(UserSuggestBox.Text)).ToList();
        }

        private void UserSuggestBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UserSuggestBox.IsSuggestionListOpen = true;
        }
    }
}
